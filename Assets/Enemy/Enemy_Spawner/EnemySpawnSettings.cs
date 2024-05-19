
using System;

[Serializable]
public class EnemySpawnSettings
{
    public int MaxEnemieCount;
    public int MinEnemiesPerSpawnWave;
    public int MaxEnemiesPerSpawnWave;
    public int MinFastEnemiesPerSpawnWave;
    public int MaxFastEnemiesPerSpawnWave;
    public float SpawnIntervalMultiplier;

    public EnemySpawnSettings(EnemySpawnSettings enemySpawnSettings)
    {
        MaxEnemieCount = enemySpawnSettings.MaxEnemieCount;
        SpawnIntervalMultiplier = enemySpawnSettings.SpawnIntervalMultiplier;
        MinEnemiesPerSpawnWave = enemySpawnSettings.MinEnemiesPerSpawnWave;
        MaxEnemiesPerSpawnWave = enemySpawnSettings.MaxEnemiesPerSpawnWave;
        MaxFastEnemiesPerSpawnWave = enemySpawnSettings.MaxFastEnemiesPerSpawnWave;
        MinFastEnemiesPerSpawnWave = enemySpawnSettings.MinFastEnemiesPerSpawnWave;
    }
}
