using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGlobalSpawnManager : MonoBehaviour
{
    [SerializeField] EnemyFactory _enemyNormalFactory;
    [SerializeField] EnemyFactory _enemyFastFactory;
    
    EnemySpawnSettings _spawnSettings;
    ISpawnPointStrategy _spawnPointStrategy;

    Transform _enemyParentTransform;

    Queue<GameObject> _inactiveSlowEnemieQueue;
    Queue<GameObject> _inactiveFastEnemieQueue;

    // Aufgaben auslagern:
    //
    // [X] Spawnposition bestimmen
    // [X] Instanzierung der Enemies
    // [ ] Objectpool 
    // [ ] Spawnen der Enemies
    // [ ] Despawnen der Enemies


    private void Awake()
    {
        _spawnSettings = FindObjectOfType<GameManagerBehavior>().EnemySpawnSettings;

        _spawnPointStrategy = new RandomSpawnPointStrategy();
        _enemyParentTransform = new GameObject("Enemies").transform;

        _inactiveSlowEnemieQueue = new Queue<GameObject>();
        _inactiveFastEnemieQueue = new Queue<GameObject>();

        InstantiateEnemies();
    }

    IEnumerator StartEnemySpawning()
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
        Vector2 groupSpawnPosition = _spawnPointStrategy.GetSpawnPoint();

        List<GameObject> enemies = GetInactiveEnemies(groupSize);
        List<Vector2> spawnPositions = GetDifferentPositionsCircle(groupSize, 0.5f);

        for (int i = 0; i < groupSize; i++)
        {
            enemies[i].transform.position = groupSpawnPosition + spawnPositions.ElementAt(i);
            enemies[i].SetActive(true);
        }
    }

    private static List<Vector2> GetDifferentPositionsCircle(int positionCount, float radius)
    {
        HashSet<Vector2> spawnPositions = new HashSet<Vector2>();

        for (int i = 0; i < positionCount; i++)
        {
            Vector2 nextSpawnPosition = Random.insideUnitCircle * radius;

            while (spawnPositions.Contains(nextSpawnPosition))
            {
                nextSpawnPosition = Random.insideUnitCircle * radius;
            }

            spawnPositions.Add(nextSpawnPosition);
        }

        return spawnPositions.ToList();
    }

    private List<GameObject> GetInactiveEnemies(int groupSize)
    {
        int fastEnemieCount = Mathf.Clamp(Random.Range(_spawnSettings.MinFastEnemiesPerSpawnWave, 1 * _spawnSettings.MaxFastEnemiesPerSpawnWave + 1), 0, groupSize);

        List<GameObject> enemies = new List<GameObject>();

        for (int i = 0; i < fastEnemieCount; i++)
        {
            if (_inactiveFastEnemieQueue.TryDequeue(out GameObject enemy))
            {
                enemies.Add(enemy);
            }
        }
       
        for (int i = 0; i < groupSize - fastEnemieCount; i++)
        {
            if (_inactiveSlowEnemieQueue.TryDequeue(out GameObject enemy))
            {
                enemies.Add(enemy);
            }
        }
        
        return enemies;
    }

    private void InstantiateEnemies()
    {
        for (int i = 0; i < _spawnSettings.MaxEnemieCount; i++)
        {
            GameObject enemy;

            if (i % 2 == 0)
            {
                enemy = _enemyFastFactory.CreateEnemy(_enemyParentTransform);
            }
            else
            {
                enemy = _enemyNormalFactory.CreateEnemy(_enemyParentTransform);
            }

            SetEnemyInactive(enemy, enemy.GetComponent<IEnemyType>().GetEnemyType());
        }
    }

    private void SetEnemyInactive(GameObject enemy, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                _inactiveSlowEnemieQueue.Enqueue(enemy);
                break;
            case EnemyType.Fast:
                _inactiveFastEnemieQueue.Enqueue(enemy);
                break;
            default:
                break;
        }

        enemy.SetActive(false);
    }

    public void DespawnEnemy(GameObject enemy)
    {
        SetEnemyInactive(enemy, enemy.GetComponent<IEnemyType>().GetEnemyType());
    }

    public void OnGameStart(Empty empty)
    {
        StartCoroutine(StartEnemySpawning());
    }
}

public interface ISpawnPointStrategy
{
    Vector2 GetSpawnPoint();
}

public class RandomSpawnPointStrategy : ISpawnPointStrategy
{
    public Vector2 GetSpawnPoint()
    {
        return Random.insideUnitCircle * 6.0f;
    }
}




