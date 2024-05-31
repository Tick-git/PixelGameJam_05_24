using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCircleEnemySpawnFormationStragegie : IEnemySpawnFormationStragegy
{
    float _circleRadius; 

    public RandomCircleEnemySpawnFormationStragegie(float circleRadius)
    {
        _circleRadius = circleRadius;
    }
    
    public List<Vector2> GetSpawnFormationPositions(int enemyCount)
    {
        HashSet<Vector2> spawnPositions = new HashSet<Vector2>();

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 nextSpawnPosition = Random.insideUnitCircle * _circleRadius;

            while (spawnPositions.Contains(nextSpawnPosition))
            {
                nextSpawnPosition = Random.insideUnitCircle * _circleRadius;
            }

            spawnPositions.Add(nextSpawnPosition);
        }

        return spawnPositions.ToList();
    }
}


