using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerStats
{
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] LayerMask _layerMask;
    
    Rigidbody2D _rigidbody;
    Animator _animator;
    Collider2D _colllider2D;
    
    float _attackCooldown;

    Vector2 _movementVector;

    PlayerSoundSystem _playerSoundSystem;

    public Vector2 MovementVector { get => _movementVector; set => _movementVector = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _colllider2D = GetComponent<CircleCollider2D>();
        _playerSoundSystem = GetComponent<PlayerSoundSystem>();
    }

    private void Update()
    {
        MovementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(KeyCode.Space) && _attackCooldown <= 0)
        {
            _attackCooldown = _playerStats.AttackCooldown;
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

            _playerSoundSystem.PlayPlayerAttackSound();
            _animator.SetTrigger("Attack");
        } 
        else
        {
            _colllider2D.enabled = false;
        }

        _animator.SetFloat("FacingDirection", MovementVector.x);
        _attackCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + MovementVector * _playerStats.Speed * Time.fixedDeltaTime);
    }

    PlayerStats IPlayerStats.GetPlayerStats()
    {
        return _playerStats;
    }
}
