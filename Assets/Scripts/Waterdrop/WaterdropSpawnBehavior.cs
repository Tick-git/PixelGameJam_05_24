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

    public void SpawnWaterdropOnGameobjectPosition(GameObject gameObject)
    {
        GameObject waterdrop = _waterdropPool.Get();

        waterdrop.transform.position = gameObject.transform.position;
    }

    internal void DespawnWaterdrop(GameObject gameObject)
    {
        _waterdropPool.Release(gameObject);
        _waterdropSoundSystem.PlayWaterdropCollectedSound();
    }
}
