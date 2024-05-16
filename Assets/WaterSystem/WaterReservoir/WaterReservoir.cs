public class WaterReservoir : IWaterReservoir
{
    private float _maxWater;
    private float _currentWater;

    public WaterReservoir(float maxWater, float startWater)
    {
        _maxWater = maxWater;
        _currentWater = startWater;
    }

    public float GetMaxCapacity()
    {
        return _maxWater;
    }

    public float GetWater(float waterAmount)
    {
        float waterToReturn = 0;

        if (_currentWater >= waterAmount)
        {
            waterToReturn = waterAmount;
        }
        else
        {
            waterToReturn = _currentWater;
        }

        _currentWater -= waterToReturn;

        return waterToReturn;
    }

    public float GetWaterStatus()
    {
        return _currentWater;
    }

    public void SetWater(float waterAmount)
    {
        if(_currentWater + waterAmount <= _maxWater)
        {
            _currentWater += waterAmount;
        }
        else
        {
            _currentWater = _maxWater;
        }
    }
}

