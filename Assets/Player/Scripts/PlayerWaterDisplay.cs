using UnityEngine;
using UnityEngine.UI;

public class PlayerWaterDisplay : MonoBehaviour
{
    [SerializeField] WaterReservoirBehavior _waterReservoir;
    Slider _slider;

    private void Awake()
    {
        _waterReservoir.OnWaterStatusChanged += OnWaterStatusChanged;

        _slider = GetComponentInChildren<Slider>();
        _slider.value = 0;
    }

    private void OnDestroy()
    {
        _waterReservoir.OnWaterStatusChanged -= OnWaterStatusChanged;
    }

    private void OnWaterStatusChanged(float newValue)
    {
        _slider.value = newValue / _waterReservoir.GetMaxCapacity();
    }
}
