using UnityEngine;

public class SteeringBehaviorBase : MonoBehaviour
{
    Rigidbody2D _rb;
    Steering[] steerings;
    
    float maxAcceleration = 10f;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        steerings = GetComponents<Steering>();
    }

    void FixedUpdate()
    {
        Vector3 accelaration = Vector3.zero;
        _rb.velocity = Vector3.zero;

        foreach (Steering behavior in steerings)
        {
            SteeringData steering = behavior.GetSteering(this);
            accelaration += steering.Linear * behavior.GetWeight();
        }

        if (accelaration.magnitude > maxAcceleration)
        {
            accelaration.Normalize();
            accelaration *= maxAcceleration;
        }

        _rb.AddForce(accelaration, ForceMode2D.Impulse);
    }

    internal Vector2 GetCurrentVelocity()
    {
        return _rb.velocity;
    }
}
