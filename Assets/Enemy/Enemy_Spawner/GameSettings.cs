using UnityEngine;

[CreateAssetMenu(menuName = "Settings/EnemySpawnSettings")]
public class GameSettings : ScriptableObject
{
    public EnemySpawnSettings EnemySpawnSettings;

    public WaterSettings WaterSettings;

    public TreeSettings TreeSettings;
}
