using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnFormationStragegy
{
    List<Vector2> GetSpawnFormationPositions(int enemyCount);
}


