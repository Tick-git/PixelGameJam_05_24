using NUnit.Framework;

public class WaterReservoirTests
{
    [Test]
    public void WaterReservoir_GetWater_IsEmpty()
    {
        WaterReservoir waterReservoir = new WaterReservoir(0,0);

        Assert.AreEqual(0, waterReservoir.GetWater(25));
        Assert.AreEqual(0, waterReservoir.GetWaterStatus());
    }

    [Test]
    public void WaterReservoir_GetWater_Full_Amount()
    {
        float startWater = 100;
        float wantedWaterAmount = 25;

        WaterReservoir waterReservoir = new WaterReservoir(100, startWater);

        Assert.AreEqual(wantedWaterAmount, waterReservoir.GetWater(wantedWaterAmount));

        Assert.AreEqual(startWater - wantedWaterAmount, waterReservoir.GetWaterStatus());
    }

    [Test]
    public void WaterReservoir_GetWater_Part_Amount()
    {
        float startWater = 20;
        float wantedWaterAmount = 23;

        WaterReservoir waterReservoir = new WaterReservoir(100, startWater);

        Assert.AreEqual(startWater, waterReservoir.GetWater(wantedWaterAmount));

        Assert.AreEqual(0, waterReservoir.GetWaterStatus());
    }
}
