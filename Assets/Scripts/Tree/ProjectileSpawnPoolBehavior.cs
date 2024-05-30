using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectSpawnPoolBehavior : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;

    Queue<GameObject> _inactiveProjectiles;

    Vector2 _projectilePositionWhileInactive;

    private void Awake()
    {
        _projectilePositionWhileInactive = new Vector2(-100, -100);
        _inactiveProjectiles = new Queue<GameObject>();

        for (int i = 0; i < 100; i++)
        {
            InstantiateProjectile();
        }
    }

    private void InstantiateProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _projectilePositionWhileInactive, Quaternion.identity, transform);

        projectile.SetActive(false);

        _inactiveProjectiles.Enqueue(projectile);
    }

    public GameObject SpawnProjectile(Vector2 position)
    {
        if (_inactiveProjectiles.Count == 0)
        {
            InstantiateProjectile();
        }

        GameObject projectile = _inactiveProjectiles.Dequeue();

        projectile.transform.position = position;
        projectile.SetActive(true);

        return projectile;
    }

    internal void DespawnProjectile(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = _projectilePositionWhileInactive;
        _inactiveProjectiles.Enqueue(gameObject);
    }
}
