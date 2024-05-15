using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyGlobalSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyEasyPrefab;
    [SerializeField] GameObject _enemyFastPrefab;
    [SerializeField] EnemySpawnSettingsSO _spawnSettings;

    Transform _enemyParentTransform;
    Vector2 _inactivePosition;

    Queue<GameObject> _inactiveEnemies;

    PlayerController _playerController;

    private void Awake()
    {
        _inactivePosition = new Vector2(-100, -100);
        _enemyParentTransform = new GameObject("Enemies").transform;

        _inactiveEnemies = new Queue<GameObject>();

        int spawnRateFast = Mathf.RoundToInt(10 / _spawnSettings.SpawnDifficultyMultiplier);

        for (int i = 0; i < _spawnSettings.MaxEnemieCount; i++)
        {
            if(i % spawnRateFast == 0)
            {
                InstantiateFastEnemy();
            } 
            else
            {
                InstantiateEasyEnemy();
            }
        }

        _playerController = FindObjectOfType<PlayerController>();
    }

    IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(10.0f / _spawnSettings.SpawnIntervalMultiplier);

        //for (int i = _spawnSettings.EnemyStartCount; i > 0;)
        //{
        //    if (i < groupSize)
        //    {
        //        groupSize = i;
        //    }

        //    SpawnEnemyGroup(groupSize);

        //    i -= groupSize;

        //    yield return wait;
        //}

        while (true)
        {
            SpawnEnemyGroup(Random.Range(1,6));

            yield return wait;
        }
    }

    private void SpawnEnemyGroup(int groupSize)
    {
        Vector2 groupSpawnPosition = GetGroupSpawnPosition();

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
            if (_inactiveEnemies.TryDequeue(out GameObject enemy))
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
        GameObject enemy = Instantiate(_enemyEasyPrefab, _inactivePosition, Quaternion.identity, _enemyParentTransform);
        SetEnemyInactive(enemy);
    }
    private void InstantiateFastEnemy()
    {
        GameObject enemy = Instantiate(_enemyFastPrefab, _inactivePosition, Quaternion.identity, _enemyParentTransform);
        SetEnemyInactive(enemy);
    }

    internal void SetEnemyInactive(GameObject enemy)
    {
        enemy.SetActive(false);
        _inactiveEnemies.Enqueue(enemy);
    }
}
