using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class EnemyGlobalSpawnManager : MonoBehaviour
{
    [SerializeField] Vector2EventChannel _enemyDespawnEvent;
    [SerializeField] EnemyFactory[] _enemyFactories;

    EnemyObjectPools _enemySpawnPools;    
    EnemySpawnSettings _spawnSettings;

    IEnemySpawnFormationStragegy _enemySpawnFormationStrategy;
    IEnemyGroupSpawnPointStrategy _enemyGroupSpawnPointStrategy;

    // Aufgaben auslagern:
    //
    // [ ] Bestimmung der Gegnerverteilung in einer Gruppe
    // [ ] Despawnen der Enemies

    private void Awake()
    {
        _spawnSettings = FindObjectOfType<GameManagerBehavior>().EnemySpawnSettings;
        _enemyGroupSpawnPointStrategy = new RandomSpawnPointStrategy();
        _enemySpawnFormationStrategy = new RandomCircleEnemySpawnFormationStragegie(0.5f);

        _enemySpawnPools = new EnemyObjectPools(_enemyFactories, _spawnSettings.MaxEnemieCount);
        _enemySpawnPools.PreInstantiateEnemies();
    }

    IEnumerator EnemySpawningLoop()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(10.0f / _spawnSettings.SpawnIntervalMultiplier);

            for (int i = 0; i < _spawnSettings.WavesPerInterval; i++)
            {
                SpawnEnemyGroup(Random.Range(_spawnSettings.MinEnemiesPerSpawnWave, _spawnSettings.MaxEnemiesPerSpawnWave + 1));
            }

            yield return wait;
        }
    }

    private void SpawnEnemyGroup(int groupSize)
    {
        Vector2 groupSpawnPosition = _enemyGroupSpawnPointStrategy.GetSpawnPoint();

        List<GameObject> enemies = GetEnemyGroup(groupSize);
        List<Vector2> enemySpawnFormationPositions = _enemySpawnFormationStrategy.GetSpawnFormationPositions(groupSize);

        for (int i = 0; i < groupSize; i++)
        {
            enemies[i].transform.position = groupSpawnPosition + enemySpawnFormationPositions.ElementAt(i);
        }
    }

    private List<GameObject> GetEnemyGroup(int groupSize)
    {
        int fastEnemieCount = Mathf.Clamp(Random.Range(_spawnSettings.MinFastEnemiesPerSpawnWave, 1 * _spawnSettings.MaxFastEnemiesPerSpawnWave + 1), 0, groupSize);
        int normalEnemieCount = groupSize - fastEnemieCount;

        List<GameObject> enemies = new List<GameObject>();

        enemies.AddRange(_enemySpawnPools.GetMultiple(EnemyType.FastEnemy, fastEnemieCount));
        enemies.AddRange(_enemySpawnPools.GetMultiple(EnemyType.NormalEnemy, normalEnemieCount));
        
        return enemies;
    }

    public void DespawnEnemy(GameObject enemy)
    {
        _enemyDespawnEvent.Invoke(enemy.transform.position);
        _enemySpawnPools.Release(enemy);
    }

    public void OnGameStart(Empty empty)
    {
        StartCoroutine(EnemySpawningLoop());
    }
}