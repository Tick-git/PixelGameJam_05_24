using UnityEngine;

public class WaterReservoirView : MonoBehaviour
{
    WaterReservoirBehavior _waterReservoir;

    private void Awake()
    {
        _waterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<WaterReservoirBehavior>();
        _waterReservoir.OnWaterStatusChanged += OnWaterStatusChanged;
    }

    private void OnWaterStatusChanged(float newStatus)
    {
        Debug.Log("Player Water: " + newStatus);
    }
}
