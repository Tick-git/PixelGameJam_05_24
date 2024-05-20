using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGlobalSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyEasyPrefab;
    [SerializeField] GameObject _enemyFastPrefab;
    
    EnemySpawnSettings _spawnSettings;

    Transform _enemyParentTransform;
    Vector2 _enemyPositionWhileInactive;

    Queue<GameObject> _inactiveSlowEnemieQueue;
    Queue<GameObject> _inactiveFastEnemieQueue;

    PlayerController _playerController;

    WaterdropSpawnpoolBehavior _waterdropManager;

    private void Awake()
    {
        _spawnSettings = FindObjectOfType<GameManagerBehavior>().EnemySpawnSettings;

        _enemyPositionWhileInactive = new Vector2(-100, -100);
        _enemyParentTransform = new GameObject("Enemies").transform;

        _inactiveSlowEnemieQueue = new Queue<GameObject>();
        _inactiveFastEnemieQueue = new Queue<GameObject>();

        _playerController = FindObjectOfType<PlayerController>();
        _waterdropManager = FindObjectOfType<WaterdropSpawnpoolBehavior>();
    }

    IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(10.0f / _spawnSettings.SpawnIntervalMultiplier);
        
        InstantiateEnemies();

        while (true)
        {
            for (int i = 0; i < _spawnSettings.WavesPerInterval; i++)
            {
                SpawnEnemyGroup(Random.Range(_spawnSettings.MinEnemiesPerSpawnWave, _spawnSettings.MaxEnemiesPerSpawnWave + 1));
            }

            yield return wait;
        }
    }    

    private void SpawnEnemyGroup(int groupSize)
    {
        Vector2 groupSpawnPosition = GetRandomGroupSpawnPosition();

        List<GameObject> enemies = GetInactiveEnemies(groupSize);
        List<Vector2> spawnPositions = GetDifferentPositionsCircle(groupSize, 0.5f);

        for (int i = 0; i < groupSize; i++)
        {
            enemies[i].transform.position = groupSpawnPosition + spawnPositions.ElementAt(i);
            enemies[i].SetActive(true);
        }
    }

    private Vector2 GetRandomGroupSpawnPosition()
    {
        return Random.insideUnitCircle * 6.0f;
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

    private Vector2 GetGroupSpawnPosition()
    {
        Vector2 playerMovementVector = _playerController.MovementVector;

        if(playerMovementVector == Vector2.zero)
        {
            playerMovementVector = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }

        Vector2 nextSpawnPos = Quaternion.Euler(0, 0, Random.Range(-45, 45)) * playerMovementVector * Random.Range(2, 6);

        return (Vector2) _playerController.transform.position + nextSpawnPos;
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
            if (i % 2 == 0)
            {
                InstantiateFastEnemy();
            }
            else
            {
                InstantiateEasyEnemy();
            }
        }
    }

    private void InstantiateEasyEnemy()
    {
        InstantiateEnemy(_enemyEasyPrefab, EnemyType.Normal);

    }
    private void InstantiateFastEnemy()
    {
        InstantiateEnemy(_enemyFastPrefab, EnemyType.Fast);
    }

    private void InstantiateEnemy(GameObject enemyPrefab, EnemyType type)
    {
        GameObject enemy = Instantiate(enemyPrefab, _enemyPositionWhileInactive, Quaternion.identity, _enemyParentTransform);

        SetEnemyInactive(enemy, type);
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

    internal void DespawnEnemy(GameObject enemy)
    {
        SetEnemyInactive(enemy, enemy.GetComponent<IEnemyType>().GetEnemyType());

        _waterdropManager.SpawnWaterdrop(enemy.transform.position);
    }
}
