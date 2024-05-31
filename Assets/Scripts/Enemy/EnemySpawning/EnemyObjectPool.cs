using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool
{
    Stack<GameObject> _inactiveEnemies;
    EnemyFactory _enemyFactory;
    Transform _enemyParentTransform;
    int _preInstantiationCount;


    public EnemyObjectPool(EnemyFactory enemyFactory, int preInstantiationCount)
    {
        _enemyFactory = enemyFactory;
        _enemyParentTransform = new GameObject(_enemyFactory.name).transform;
        _preInstantiationCount = preInstantiationCount;
        _inactiveEnemies = new Stack<GameObject>(preInstantiationCount);
    }

    public int CountInactive => _inactiveEnemies.Count;

    public void PreInstantiateEnemies()
    {
        for (int i = 0; i < _preInstantiationCount; i++)
        {
            Release(_enemyFactory.CreateEnemy(_enemyParentTransform));
        }
    }

    public void Clear()
    {
        foreach (var enemy in _inactiveEnemies)
        {
            Object.Destroy(enemy);
        }

        _inactiveEnemies.Clear();
    }

    public GameObject Get()
    {
        GameObject enemy;

        if (_inactiveEnemies.Count == 0)
        {
            _inactiveEnemies.Push(_enemyFactory.CreateEnemy(_enemyParentTransform));
        }

        enemy = _inactiveEnemies.Pop();
        enemy.SetActive(true);

        return enemy;
    }

    public void Release(GameObject enemy)
    {
        enemy.SetActive(false);

        _inactiveEnemies.Push(enemy);
    }

    public GameObject[] GetMultiple(int count)
    {
        GameObject[] enemies = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            enemies[i] = Get();
        }

        return enemies;
    }
}


