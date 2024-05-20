using UnityEngine;

[CreateAssetMenu(menuName = "Settings/EnemySpawnSettings")]
public class GameSettings : ScriptableObject
{
    public EnemySpawnSettings EnemySpawnSettings;

    public WaterSettings WaterSettings;

    public TreeSettings TreeSettings;

    public DifficultySettings DifficultySettingsEvery15Seconds;
    public DifficultySettings DifficultySettingsEvery30Seconds;
    public DifficultySettings DifficultySettingsEvery60Seconds;
}

