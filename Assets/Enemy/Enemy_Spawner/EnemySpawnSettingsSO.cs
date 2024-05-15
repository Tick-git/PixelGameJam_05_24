using UnityEngine;

[CreateAssetMenu(menuName = "Settings/EnemySpawnSettings")]
public class EnemySpawnSettingsSO : ScriptableObject
{
    public int EnemyStartCount;
    public int MaxEnemieCount;
    public float SpawnIntervalMultiplier;
    public float SpawnDifficultyMultiplier;
}
