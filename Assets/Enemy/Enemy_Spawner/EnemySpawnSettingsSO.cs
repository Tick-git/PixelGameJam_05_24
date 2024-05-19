using UnityEngine;

[CreateAssetMenu(menuName = "Settings/EnemySpawnSettings")]
public class EnemySpawnSettingsSO : ScriptableObject
{
    public int MaxEnemieCount;
    public float SpawnIntervalMultiplier;
    
    public int MinEnemiesPerSpawnWave;
    public int MaxEnemiesPerSpawnWave;
    public int MaxFastEnemiesPerSpawnWave;
    public int MinFastEnemiesPerSpawnWave;

}
