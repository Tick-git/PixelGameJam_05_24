using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    [SerializeField] EnemyGlobalSpawnManager _enemyGlobalSpawnManager;
    [SerializeField] GameSettings _gameSettings;

    PlayerHealthSystem _playerHealthSystem;
    WaterReservoirBehavior _waterReservoirBehavior;

    public EnemySpawnSettings EnemySpawnSettings { get; private set; }
    public WaterSettings WaterSettings { get; private set; }

    public TreeSettings TreeSettings { get; private set; }

    private void Awake()
    {
        EnemySpawnSettings = new EnemySpawnSettings(_gameSettings.EnemySpawnSettings);
        WaterSettings = new WaterSettings(_gameSettings.WaterSettings);
        TreeSettings = new TreeSettings(_gameSettings.TreeSettings);

        _playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        _waterReservoirBehavior = TransformHelper.FindRootTransform(FindObjectOfType<OasisSpreadBehavior>().transform).GetComponentInChildren<WaterReservoirBehavior>();

        _playerHealthSystem.OnHealthChanged += OnPlayerHealthChanged;
        _waterReservoirBehavior.OnWaterStatusChanged += OnOasisWaterStatusChanged;

        Instantiate(_enemyGlobalSpawnManager);
    }

    private void OnDestroy()
    {
        _playerHealthSystem.OnHealthChanged -= OnPlayerHealthChanged;
        _waterReservoirBehavior.OnWaterStatusChanged -= OnOasisWaterStatusChanged;
    }

    private void OnOasisWaterStatusChanged(float newWaterStatus)
    {
        if(newWaterStatus >= _gameSettings.WaterSettings.MaxOasisWaterCapacity)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnPlayerHealthChanged(float newHealth)
    {
        if(newHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(15);

        while(true)
        {
            yield return wait;

            EnemySpawnSettings.SpawnIntervalMultiplier += 0.5f;
            EnemySpawnSettings.MaxEnemiesPerSpawnWave += 1;
            EnemySpawnSettings.MinEnemiesPerSpawnWave += 1;

            yield return wait;

            EnemySpawnSettings.MaxFastEnemiesPerSpawnWave += 1;
            EnemySpawnSettings.MinFastEnemiesPerSpawnWave += 1;
        }
    }

}
