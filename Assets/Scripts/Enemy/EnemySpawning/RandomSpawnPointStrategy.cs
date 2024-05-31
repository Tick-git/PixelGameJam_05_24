using UnityEngine;

public class RandomSpawnPointStrategy : IEnemyGroupSpawnPointStrategy
{
    public Vector2 GetSpawnPoint()
    {
        return Random.insideUnitCircle * 6.0f;
    }
}


