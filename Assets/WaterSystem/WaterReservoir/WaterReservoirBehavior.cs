using System;
using UnityEngine;

public class WaterReservoirBehavior : MonoBehaviour, IWaterReservoir
{
    [SerializeField] float _maxWater = 100f;
    [SerializeField] float _startWater = 100f;

    IWaterReservoir _waterReservoir;

    public Action<float> OnWaterStatusChanged;

    private void Start()
    {
        _waterReservoir = new WaterReservoir(_maxWater, _startWater);
    }

    public float GetWater(float waterAmount)
    {
        float newWaterAmount = _waterReservoir.GetWater(waterAmount);

        OnWaterStatusChanged?.Invoke(_waterReservoir.GetWaterStatus());

        return newWaterAmount;
    }

    public float GetWaterStatus()
    {
        return _waterReservoir.GetWaterStatus();
    }

    public void SetWater(float waterAmount)
    {
        _waterReservoir.SetWater(waterAmount);

        OnWaterStatusChanged?.Invoke(_waterReservoir.GetWaterStatus());
    }

    public float GetMaxCapacity()
    {
        return _waterReservoir.GetMaxCapacity();    
    }
}

