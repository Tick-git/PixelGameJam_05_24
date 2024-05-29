using UnityEngine;
using UnityEngine.UI;

public class WaterStatusDisplay : MonoBehaviour
{
    [SerializeField] WaterReservoirBehavior _waterReservoir;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _slider.value = 0;
    }

    public void OnWaterStatusChanged(float newValue)
    {
        _slider.value = newValue / _waterReservoir.GetMaxCapacity();
    }
}
