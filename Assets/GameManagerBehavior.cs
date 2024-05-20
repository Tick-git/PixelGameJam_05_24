using System;
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

    void Start()
    {
        StartCoroutine(IncreaseDifficulty(new WaitForSeconds(15), _gameSettings.DifficultySettingsEvery15Seconds));
        StartCoroutine(IncreaseDifficulty(new WaitForSeconds(30), _gameSettings.DifficultySettingsEvery30Seconds));
        StartCoroutine(IncreaseDifficulty(new WaitForSeconds(60), _gameSettings.DifficultySettingsEvery60Seconds));
    }

    private IEnumerator IncreaseDifficulty(WaitForSeconds wait, DifficultySettings difficultySettings)
    {
        while (true)
        {
            yield return wait;

            EnemySpawnSettings.SpawnIntervalMultiplier += difficultySettings.SpawnIntervalMultiplierIncrease;
            EnemySpawnSettings.WavesPerInterval += difficultySettings.WavesPerIntervalIncrease;

            EnemySpawnSettings.MinEnemiesPerSpawnWave += difficultySettings.MinEnemiesPerSpawnWaveIncrease;
            EnemySpawnSettings.MaxEnemiesPerSpawnWave += difficultySettings.MaxEnemiesPerSpawnWaveIncrease;

            EnemySpawnSettings.MinFastEnemiesPerSpawnWave += difficultySettings.MinFastEnemiesPerSpawnWaveIncrease;
            EnemySpawnSettings.MaxFastEnemiesPerSpawnWave += difficultySettings.MaxFastEnemiesPerSpawnWaveIncrease;
        }
    }   
}
