using UnityEngine;

public abstract class EnemyFactory : ScriptableObject
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] protected GameObject _enemyPrefab;
    [SerializeField] protected Vector3 _spawnPosition = new Vector2(-100,100);

    public EnemyType EnemyType => _enemyType;

    public abstract GameObject CreateEnemy(Transform parent);


}




