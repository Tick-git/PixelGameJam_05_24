using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPools
{
    Dictionary<EnemyType, EnemyObjectPool> _enemyObjectPools;

    public EnemyObjectPools(EnemyFactory[] enemyFactories, int preInstantiationCount)
    {
        _enemyObjectPools = new Dictionary<EnemyType, EnemyObjectPool>();

        foreach (var factory in enemyFactories)
        {
            _enemyObjectPools[factory.EnemyType] = new EnemyObjectPool(factory, preInstantiationCount);
        }
    }

    public void PreInstantiateEnemies()
    {
        foreach (var pool in _enemyObjectPools.Values)
        {
            pool.PreInstantiateEnemies();
        }
    }

    public void Clear()
    {
        foreach (var pool in _enemyObjectPools.Values)
        {
            pool.Clear();
        }
    }

    public GameObject Get(EnemyType type)
    {
        return _enemyObjectPools[type].Get();
    }

    public GameObject[] GetMultiple(EnemyType type, int count)
    {
        return _enemyObjectPools[type].GetMultiple(count);
    }

    public void Release(GameObject enemy)
    {
        EnemyType type = enemy.GetComponent<IEnemyType>().GetEnemyType();

        _enemyObjectPools[type].Release(enemy);
    }
}


