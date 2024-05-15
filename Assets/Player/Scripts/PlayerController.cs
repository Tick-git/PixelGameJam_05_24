using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] LayerMask _layerMask;
    
    Rigidbody2D _rigidbody;
    Animator _animator;
    Collider2D _colllider2D;
    
    Vector2 _movementVector;
    bool _attack;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _colllider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            List<Collider2D> colliders = new List<Collider2D>();
            _colllider2D.enabled = true;
            _colllider2D.OverlapCollider(new ContactFilter2D() { useTriggers = true, useLayerMask = true, layerMask = _layerMask}, colliders);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable enemy))
                {
                    enemy.TakeDamage(10, transform.position);
                }
            }
        }
        else
        {
            _colllider2D.enabled = false;   
        }

        _animator.SetFloat("FacingDirection", _movementVector.x);
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movementVector * _playerStats.Speed * Time.fixedDeltaTime);
    }
}