using UnityEngine;

public class PlayerWaterReservoirCapacity : MonoBehaviour, IWaterCapacity
{
    public float GetMaxCapacity()
    {
        return FindObjectOfType<GameManagerBehavior>().WaterSettings.MaxPlayerWaterCapacity;
    }
}
