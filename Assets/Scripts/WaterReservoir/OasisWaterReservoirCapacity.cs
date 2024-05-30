using UnityEngine;

public class OasisWaterReservoirCapacity : MonoBehaviour, IWaterCapacity
{
    public float GetMaxCapacity()
    {
        return FindObjectOfType<GameManagerBehavior>().WaterSettings.MaxOasisWaterCapacity;
    }
}
