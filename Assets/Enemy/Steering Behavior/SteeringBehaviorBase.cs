using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviorBase : MonoBehaviour
{
    Rigidbody2D _rb;
    Steering[] steerings;
    
    float maxAcceleration = 10f;
    float maxAngularAcceleration = 3f;
    
    float drag = 1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        steerings = GetComponents<Steering>();
        _rb.drag = drag;
    }

    void FixedUpdate()
    {
        Vector3 accelaration = Vector3.zero;

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

        _rb.AddForce(accelaration);
    }
}
