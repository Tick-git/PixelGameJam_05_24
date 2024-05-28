using System;

internal interface IWaterReservoir
{
    public void SetWater(float waterAmount);
    public float GetWater(float waterAmount);
    public float GetWaterStatus();
    public float GetMaxCapacity();
}