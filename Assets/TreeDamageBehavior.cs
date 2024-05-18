using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _attackRadiusCollider = GetComponent<Collider2D>();
        _cooldownBar = GetComponentInChildren<Scrollbar>();
    }

    private void OnEnable()
    {
        StartCoroutine(DoDamage());
    }

    private IEnumerator DoDamage()
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