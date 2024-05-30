using System;
using UnityEngine;

[Serializable]
public class WaterSettings
{
    public float MaxOasisWaterCapacity;
    public float MaxPlayerWaterCapacity;

    public WaterSettings(WaterSettings waterSettings)
    {
        MaxOasisWaterCapacity = waterSettings.MaxOasisWaterCapacity;
        MaxPlayerWaterCapacity = waterSettings.MaxPlayerWaterCapacity;
    }
}


