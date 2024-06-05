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
    [SerializeField] GameObject _projctile;

    private static ProjectileSpawnPoolBehavior _projectSpawnPoolBehavior;

    Collider2D _attackRadiusCollider;

    Scrollbar _cooldownBar;

    HashSet<IDamageable> _targets;

    private void Awake()
    {
        if (_projectSpawnPoolBehavior == null)
        {
            _projectSpawnPoolBehavior = FindObjectOfType<ProjectileSpawnPoolBehavior>();
        }

        _attackRadiusCollider = GetComponent<Collider2D>();
        _cooldownBar = GetComponentInChildren<Scrollbar>();
        _targets = new HashSet<IDamageable>();
    }

    private void OnEnable()
    {
        StartCoroutine(DoDamage());
    }

    private void Start()
    {
        TreeSettings treeSettings = FindAnyObjectByType<GameManagerBehavior>().TreeSettings;

        _damageIntervalInSeconds = treeSettings.TreeShootInterval;
        _treeDamage = (int) treeSettings.TreeDamage;
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
                _projectSpawnPoolBehavior.SpawnProjectile(transform.position + Vector3.up * 0.5f).GetComponent<ProjectileBehavior>().Shoot(target.transform,_treeDamage);
                t = 0;
            }

            t += Time.deltaTime / _damageIntervalInSeconds;

            yield return null;
        }
    }
}
