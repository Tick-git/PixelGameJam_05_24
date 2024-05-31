using UnityEngine;

[CreateAssetMenu(menuName = "EnemyFactories/NormalEnemyFactory")]
public class NormalEnemyFactory : EnemyFactory
{
    public override GameObject CreateEnemy(Transform parent)
    {
        return Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity, parent);
    }
}




