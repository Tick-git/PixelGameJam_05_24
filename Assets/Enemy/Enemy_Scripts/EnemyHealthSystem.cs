using System.Collections;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable 
{
    [SerializeField] float _maxHealth = 20;
    [SerializeField] float _knockbackForce = 10f;

    Rigidbody2D _rb;

    SpriteRenderer _spriteRenderer;

    EnemyGlobalSpawnManager _enemySpawnManager;

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
        _enemySpawnManager = FindObjectOfType<EnemyGlobalSpawnManager>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        StopAllCoroutines();

        StartCoroutine(HandleDamageTook(damage, origin));
    }

    private IEnumerator HandleDamageTook(int damage, Vector3 origin)
    {
        AddKnockbackToEnemy(origin);

        _currentHealth -= damage;

        Color colorBeforeFlashRed = _spriteRenderer.color;

        _steeringBehaviorBase.enabled = false;
        _spriteRenderer.color = new Color(1, 0.75f, 0.75f);

        yield return new WaitForSeconds(0.1f);

        _rb.velocity = Vector2.zero;
        _spriteRenderer.color = colorBeforeFlashRed;
        _steeringBehaviorBase.enabled = true;

        if (_currentHealth <= 0)
        {
            _enemySpawnManager.SetEnemyInactive(gameObject);
            _currentHealth = _maxHealth;
        }
    }

    private void AddKnockbackToEnemy(Vector3 origin)
    {
        _rb.AddForce((transform.position - origin).normalized * _knockbackForce, ForceMode2D.Impulse);
    }
}
