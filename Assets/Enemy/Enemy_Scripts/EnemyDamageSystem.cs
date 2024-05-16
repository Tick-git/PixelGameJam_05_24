using System.Collections;
using UnityEngine;

public class EnemyDamageSystem : MonoBehaviour
{
    Transform _target;

    SteeringBehaviorBase _steeringBehaviorBase;

    Rigidbody2D _rb;
    private Coroutine _coroutine;

    private void Start()
    {
        _target = GetComponent<EnemyTargetBehavior>().Target;
        _steeringBehaviorBase = GetComponent<SteeringBehaviorBase>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(_coroutine == null && Vector2.Distance(transform.position, _target.position) <= 0.15f)
        {
            _target.GetComponent<IDamageable>().TakeDamage(10, transform.position);
            _steeringBehaviorBase.enabled = false;
            _rb.velocity = Vector2.zero;
            _coroutine = StartCoroutine(EnableSteering());
        }
    }

    private IEnumerator EnableSteering()
    {
        yield return new WaitForSeconds(0.5f);

        _steeringBehaviorBase.enabled = true;
        _coroutine = null;
    }
}
