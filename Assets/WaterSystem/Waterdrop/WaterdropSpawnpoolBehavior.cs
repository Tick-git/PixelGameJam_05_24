using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterdropSpawnpoolBehavior : MonoBehaviour
{
    [SerializeField] GameObject waterdropPrefab;

    Vector2 _waterdropPositionWhileInactive;

    Queue<GameObject> _inactiveWaterdropsQueue;

    Transform _waterdropParent;

    WaterdropSoundSystem _waterdropSoundSystem;

    private void Awake()
    {
        _waterdropPositionWhileInactive = new Vector2(-100, -100);
        _inactiveWaterdropsQueue = new Queue<GameObject>();
        _waterdropParent = new GameObject("Waterdrops").transform;
        _waterdropSoundSystem = FindObjectOfType<WaterdropSoundSystem>();

        
    }

    private void Start()
    {
        EnemySpawnSettings EnemySpawnSettings = FindObjectOfType<GameManagerBehavior>().EnemySpawnSettings;

        for (int i = 0; i < EnemySpawnSettings.MaxEnemieCount * 1.5f; i++)
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

        _waterdropSoundSystem.PlayWaterdropCollectedSound();
        _inactiveWaterdropsQueue.Enqueue(gameObject);
    }
}
