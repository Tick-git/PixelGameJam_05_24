using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(Transform parent);
    EnemyType EnemyType { get; }
}




