using System;
using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform target;
    public float maxSpeed = 5f;
    [SerializeField] float _threshold = 0.1f;

    private void Start()
    {
        EnemyTargetBehavior enemyTargetBehavior = GetComponent<EnemyTargetBehavior>();
        target = enemyTargetBehavior.Target;

        enemyTargetBehavior.OnTargetChanged += OnTargetChanged;
    }

    private void OnDestroy()
    {
        GetComponent<EnemyTargetBehavior>().OnTargetChanged -= OnTargetChanged;
    }

    private void OnTargetChanged()
    {
        throw new NotImplementedException();
    }

    public override SteeringData GetSteering(SteeringBehaviorBase steeringbase)
    {
        SteeringData steering = new SteeringData();

        Vector3 direction = target.position - transform.position;

        if(direction.magnitude < _threshold)
        {
            return steering;
        }

        direction.Normalize();
        steering.Linear = direction * maxSpeed;

        return steering;
    }
}

