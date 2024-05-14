using System.Collections.Generic;
using UnityEngine;

public class SeparationBehavior : Steering
{
    Collider2D _collider;

    [SerializeField] LayerMask _layerMask;

    [SerializeField] private float threshold = 2f;
    [SerializeField] private float decayCoefficient = -25f;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        SteeringBehaviorBase[] agents = FindObjectsOfType<SteeringBehaviorBase>();
    }

    public override SteeringData GetSteering(SteeringBehaviorBase steeringbase)
    {
        SteeringData steering = new SteeringData();

        List<Collider2D> collider2Ds = new List<Collider2D>();

        _collider.OverlapCollider(new ContactFilter2D() { useTriggers = true, layerMask = _layerMask, useLayerMask = true}, collider2Ds); ;

        foreach (Collider2D target in collider2Ds)
        {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                float strength = decayCoefficient / (distance * distance);
                direction.Normalize();
                steering.Linear += strength * direction;
            }
        }

        return steering;
    }
}

