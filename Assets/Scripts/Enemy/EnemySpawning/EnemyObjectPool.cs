using UnityEngine;

public class EnemyObjectPool : GameObjectPool
{
    IEnemyFactory _enemyFactory;
    Transform _enemyParentTransform;

    public EnemyObjectPool(IEnemyFactory enemyFactory, int preInstantiationCount) : base(preInstantiationCount)
    {
        _enemyFactory = enemyFactory;
        _enemyParentTransform = new GameObject(_enemyFactory.EnemyType.ToString()).transform;
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

    protected override GameObject CreateObject()
    {
        return _enemyFactory.CreateEnemy(_enemyParentTransform);
    }
}


