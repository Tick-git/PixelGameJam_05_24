using System;

internal interface IWaterReservoir
{
    public float GetWaterStatus();
    public void SetWater(float waterAmount);
    public float GetWater(float waterAmount);
}