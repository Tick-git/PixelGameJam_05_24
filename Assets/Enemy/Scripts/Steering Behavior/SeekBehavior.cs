using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform target;
    public float maxSpeed = 5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override SteeringData GetSteering(SteeringBehaviorBase steeringbase)
    {
        SteeringData steering = new SteeringData();

        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        steering.Linear = direction * maxSpeed;

        return steering;
    }
}

