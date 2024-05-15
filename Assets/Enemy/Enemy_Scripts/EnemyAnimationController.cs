using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator _animationController;
    Rigidbody2D _rb;

    Transform _target;

    private void Awake()
    {
        _animationController = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        Vector2 targetDirection = _target.position - transform.position;

        _animationController.SetFloat("FacingDirection", targetDirection.x);
    }
}
