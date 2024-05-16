using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterdropManagerBehavior : MonoBehaviour
{
    [SerializeField] GameObject waterdropPrefab;
    [SerializeField] EnemySpawnSettingsSO enemySpawnSettings;

    Vector2 _waterdropPositionWhileInactive;

    Queue<GameObject> _inactiveWaterdropsQueue;

    Transform _waterdropParent;

    private void Awake()
    {
        _waterdropPositionWhileInactive = new Vector2(-100, -100);
        _inactiveWaterdropsQueue = new Queue<GameObject>();
        _waterdropParent = new GameObject("Waterdrops").transform;
    }

    void Start()
    {
        for (int i = 0; i < enemySpawnSettings.MaxEnemieCount * 1.5f; i++)
        {
            InstantiateWaterdrop();
        }
    }

    private void InstantiateWaterdrop()
    {
        GameObject waterdrop = Instantiate(waterdropPrefab, _waterdropPositionWhileInactive, Quaternion.identity, _waterdropParent);

        waterdrop.SetActive(false);

        _inactiveWaterdropsQueue.Enqueue(waterdrop);
    }

    public void SpawnWaterdrop(Vector2 position)
    {
        if (_inactiveWaterdropsQueue.Count == 0)
        {
            InstantiateWaterdrop();
        }

        GameObject waterdrop = _inactiveWaterdropsQueue.Dequeue();

        waterdrop.transform.position = position;
        waterdrop.SetActive(true);
    }

    internal void DespawnWaterdrop(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = _waterdropPositionWhileInactive;

        _inactiveWaterdropsQueue.Enqueue(gameObject);
    }
}
