using System;
using System.Collections;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable 
{
    [SerializeField] float _maxHealth = 20;
    [SerializeField] float _knockbackForce = 10f;

    Rigidbody2D _rb;

    SpriteRenderer _spriteRenderer;

    EnemySpawnManager _enemySpawnManager;

    SteeringBehaviorBase _steeringBehaviorBase;

    float _currentHealth;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();    
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _steeringBehaviorBase = GetComponent<SteeringBehaviorBase>();
    }

    private void Start()
    {
        _enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        StopAllCoroutines();

        _currentHealth -= damage;

        _rb.AddForce((transform.position - origin).normalized * _knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        _steeringBehaviorBase.enabled = false;

        Color colorBeforeFlashRed = _spriteRenderer.color;
        _spriteRenderer.color = new Color(1, 0.75f, 0.75f);

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = colorBeforeFlashRed;
        _rb.velocity = Vector2.zero;

        _steeringBehaviorBase.enabled = true;

        if(_currentHealth <= 0)
        {
            _enemySpawnManager.SetEnemyInactive(gameObject);
            _currentHealth = _maxHealth;
        }
    }
}
