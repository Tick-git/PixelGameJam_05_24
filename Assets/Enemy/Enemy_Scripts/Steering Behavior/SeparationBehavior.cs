using System.Collections.Generic;
using UnityEngine;

public class SeparationBehavior : Steering
{
    [SerializeField] LayerMask _seperateFromLayer;
    [SerializeField] float threshold = 2f;
    [SerializeField] float decayCoefficient = -25f;

    Collider2D _collider;

    ContactFilter2D _contactFilter;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _contactFilter = new ContactFilter2D() { useTriggers = true, layerMask = _seperateFromLayer, useLayerMask = true };
    }

    public override SteeringData GetSteering(SteeringBehaviorBase steeringbase)
    {
        SteeringData steering = new SteeringData();

        List<Collider2D> enemiesInRange = new List<Collider2D>();

        _collider.OverlapCollider(_contactFilter, enemiesInRange);

        foreach (Collider2D target in enemiesInRange)
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget < threshold)
            {
                float strength = decayCoefficient / (distanceToTarget * distanceToTarget);
                directionToTarget.Normalize();
                steering.Linear += strength * directionToTarget;
            }
        }

        return steering;
    }
}

