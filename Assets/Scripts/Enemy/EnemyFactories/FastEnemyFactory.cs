using UnityEngine;

[CreateAssetMenu(menuName = "EnemyFactories/FastEnemyFactory")]
public class FastEnemyFactory : EnemyFactory
{
    public override GameObject CreateEnemy(Transform parent)
    {
        return Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity, parent);
    }
}




