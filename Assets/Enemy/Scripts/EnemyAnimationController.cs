using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator _animationController;
    Rigidbody2D _rb;

    private void Awake()
    {
        _animationController = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _animationController.SetFloat("FacingDirection", _rb.velocity.x);
    }
}
