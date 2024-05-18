using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TreeDamageBehavior : MonoBehaviour
{
    [SerializeField] int _treeDamage = 10;
    [SerializeField] float _damageIntervalInSeconds = 1f;
    [SerializeField] LayerMask _enemyLayerMask;

    Collider2D _attackRadiusCollider;

    Scrollbar _cooldownBar;

    HashSet<IDamageable> _targets;

    private void Awake()
    {
        enabled = false;
        _attackRadiusCollider = GetComponent<Collider2D>();
        _cooldownBar = GetComponentInChildren<Scrollbar>();
        _targets = new HashSet<IDamageable>();
    }

    private void OnEnable()
    {
        StartCoroutine(DoDamage());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out IDamageable damageable))
        {
            _targets.Add(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out IDamageable damageable))
        {
            _targets.Remove(damageable);
        }
    }

    private IEnumerator DoDamage()
    {
        MonoBehaviour target = null;
        float t = 0;

        while (true)
        {          
            if(_targets.Count > 0 && t > 1)
            {
                target = (MonoBehaviour) _targets.ElementAt(0);
                _targets.ElementAt(0).TakeDamage(_treeDamage, transform.position);
                t = 0;
            }

            if(_targets.Count > 0 && target != null)
            {
                Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
            }

            t += Time.deltaTime / _damageIntervalInSeconds;

            yield return null;
        }
    }

    private IEnumerator DoDamageAround()
    {
        yield return new WaitForSeconds(Random.Range(0, _damageIntervalInSeconds));

        while (true)
        {
            float t = 0;

            while(t < 1)
            {
                _cooldownBar.size = t;
                t += Time.deltaTime / _damageIntervalInSeconds;
                yield return null;
            }

            List<Collider2D> hitColliders = new(); 
            _attackRadiusCollider.OverlapCollider(new ContactFilter2D() { useTriggers = true, useLayerMask = true, layerMask = _enemyLayerMask}, hitColliders);

            foreach (var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<IDamageable>().TakeDamage(_treeDamage, transform.position);
            }
        }
    }
}