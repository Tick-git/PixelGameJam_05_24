using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGlobalSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyEasyPrefab;
    [SerializeField] GameObject _enemyFastPrefab;
    [SerializeField] EnemySpawnSettingsSO _spawnSettings;

    Transform _enemyParentTransform;
    Vector2 _enemyPositionWhileInactive;

    Queue<GameObject> _inactiveEnemiesQueue;

    PlayerController _playerController;

    WaterdropManagerBehavior _waterdropManager;

    private void Awake()
    {
        _enemyPositionWhileInactive = new Vector2(-100, -100);
        _enemyParentTransform = new GameObject("Enemies").transform;
        _inactiveEnemiesQueue = new Queue<GameObject>();
        _playerController = FindObjectOfType<PlayerController>();
        _waterdropManager = FindObjectOfType<WaterdropManagerBehavior>();
    }

    IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(10.0f / _spawnSettings.SpawnIntervalMultiplier);
        
        InstantiateEnemies();

        while (true)
        {
            SpawnEnemyGroup(Random.Range(1, 6));

            yield return wait;
        }
    }

    private void InstantiateEnemies()
    {
        int spawningRateForFastEnemies = Mathf.RoundToInt(10 / _spawnSettings.SpawnDifficultyMultiplier);

        for (int i = 0; i < _spawnSettings.MaxEnemieCount; i++)
        {
            if (i % spawningRateForFastEnemies == 0)
            {
                InstantiateFastEnemy();
            }
            else
            {
                InstantiateEasyEnemy();
            }
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
        List<GameObject> enemies = new List<GameObject>();

        for (int i = 0; i < groupSize; i++)
        {
            if (_inactiveEnemiesQueue.TryDequeue(out GameObject enemy))
            {
                enemies.Add(enemy);
            }
        }

        if(enemies.Count < groupSize)
        {
            Debug.Log("Not enough enemies in pool");    
        }   

        return enemies;
    }

    private void InstantiateEasyEnemy()
    {
        InstantiateEnemy(_enemyEasyPrefab);

    }
    private void InstantiateFastEnemy()
    {
        InstantiateEnemy(_enemyFastPrefab);
    }

    private void InstantiateEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, _enemyPositionWhileInactive, Quaternion.identity, _enemyParentTransform);

        SetEnemyInactive(enemy);
    }

    private void SetEnemyInactive(GameObject enemy)
    {
        enemy.SetActive(false);
        _inactiveEnemiesQueue.Enqueue(enemy);
    }

    internal void DespawnEnemy(GameObject enemy)
    {
        SetEnemyInactive(enemy);

        _waterdropManager.SpawnWaterdrop(enemy.transform.position);
    }
}
