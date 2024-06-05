using UnityEngine;

public class WaterdropSpawnBehavior : MonoBehaviour
{
    [SerializeField] WaterdropFactory _waterdropFactory;

    WaterdropPool _waterdropPool;

    WaterdropSoundSystem _waterdropSoundSystem;

    private void Awake()
    {
        _waterdropSoundSystem = FindObjectOfType<WaterdropSoundSystem>();      

        EnemySpawnSettings enemySpawnSettings = FindObjectOfType<GameManagerBehavior>().EnemySpawnSettings;

        _waterdropPool = new WaterdropPool(_waterdropFactory, enemySpawnSettings.MaxEnemieCount);
        _waterdropPool.InstantiateGameObjects();
    }

    public void SpawnWaterdropOnGameobjectPosition(Vector2 spawnPosition)
    {
        _waterdropPool.Get().transform.position = spawnPosition;
    }

    internal void DespawnWaterdrop(GameObject gameObject)
    {
        _waterdropPool.Release(gameObject);
        _waterdropSoundSystem.PlayWaterdropCollectedSound();
    }
}
