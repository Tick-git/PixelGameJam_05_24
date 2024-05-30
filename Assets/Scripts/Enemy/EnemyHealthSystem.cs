using System.Collections;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable, IEnemyType
{
    [SerializeField] float _maxHealth = 20;
    [SerializeField] float _knockbackForce = 10f;
    [SerializeField] EnemyType _enemyType;
    [SerializeField] GameObjectEventChannel _onEnemyDeath;

    Rigidbody2D _rb;

    SpriteRenderer _spriteRenderer;

    SteeringBehaviorBase _steeringBehaviorBase;

    float _currentHealth;

    EnemyDamageSystem _enemyDamageSystem;

    Color _startColor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();    
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _steeringBehaviorBase = GetComponent<SteeringBehaviorBase>();
        _enemyDamageSystem = GetComponent<EnemyDamageSystem>();
        _startColor = _spriteRenderer.color;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        StartCoroutine(HandleDamageTook(damage, origin));
    }

    private IEnumerator HandleDamageTook(int damage, Vector3 origin)
    {
        AddKnockbackToEnemy(origin, damage);

        _currentHealth -= damage;

        _steeringBehaviorBase.enabled = false;
        _spriteRenderer.color = new Color(1, 0.75f, 0.75f);
        _enemyDamageSystem.enabled = false;

        yield return new WaitForSeconds(0.1f);

        if (_currentHealth <= 0)
        {
            StopAllCoroutines();
            _onEnemyDeath.Invoke(gameObject);
            _currentHealth = _maxHealth;
            _steeringBehaviorBase.enabled = false;
            _enemyDamageSystem.enabled = false;
        } 
        else
        {
            _steeringBehaviorBase.enabled = true;
            _enemyDamageSystem.enabled = true;     
        }

        StopKnockbbackFromEnemy(damage);
        _spriteRenderer.color = _startColor;
    }

    private void StopKnockbbackFromEnemy(float damage)
    {
        if (damage < 10) return;

        _rb.velocity = Vector2.zero;
    }

    private void AddKnockbackToEnemy(Vector3 origin, float damage)
    {
        if (damage < 10) return;

        _rb.AddForce((transform.position - origin).normalized * _knockbackForce, ForceMode2D.Impulse);
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
