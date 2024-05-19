using System;
using UnityEngine;

public class WaterReservoirBehavior : MonoBehaviour, IWaterReservoir
{
    [SerializeField] float _startWater;

    IWaterReservoir _waterReservoir;

    public Action<float> OnWaterStatusChanged;


    private void Start()
    {
        IWaterCapacity waterCapacity = TransformHelper.FindRootTransform(transform).GetComponentInChildren<IWaterCapacity>();
        _waterReservoir = new WaterReservoir(waterCapacity.GetMaxCapacity(), _startWater);
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

