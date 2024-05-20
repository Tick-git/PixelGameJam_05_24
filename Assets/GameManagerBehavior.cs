using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

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
            GameEnd(true);
        }
    }

    private void GameEnd(bool playerHasWon)
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("root").style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        
        if(playerHasWon)
        {
            root.Q<VisualElement>("YouWon").style.display = DisplayStyle.Flex;
            root.Q<VisualElement>("GameOver").style.display = DisplayStyle.None;
        } 
        else
        {
            root.Q<VisualElement>("YouWon").style.display = DisplayStyle.None;
            root.Q<VisualElement>("GameOver").style.display = DisplayStyle.Flex;
        }

        root.Q<Button>("RestartButton").clicked += RestartClicked;
    }

    private void RestartClicked()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("RestartButton").clicked -= RestartClicked;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void OnPlayerHealthChanged(float newHealth)
    {
        if(newHealth <= 0)
        {
            GameEnd(false);
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

    internal void StartGame()
    {
        Instantiate(_enemyGlobalSpawnManager);

        _playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        _waterReservoirBehavior = TransformHelper.FindRootTransform(FindObjectOfType<OasisSpreadBehavior>().transform).GetComponentInChildren<WaterReservoirBehavior>();

        _playerHealthSystem.OnHealthChanged += OnPlayerHealthChanged;
        _waterReservoirBehavior.OnWaterStatusChanged += OnOasisWaterStatusChanged;
    }
}
