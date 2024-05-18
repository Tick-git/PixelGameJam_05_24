using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameendBehavior : MonoBehaviour
{
    WaterReservoirBehavior _waterReservoir;

    private void Awake()
    {
        _waterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<WaterReservoirBehavior>();
        _waterReservoir.OnWaterStatusChanged += OnWaterStatusChanged;
    }

    private void OnDestroy()
    {
        _waterReservoir.OnWaterStatusChanged -= OnWaterStatusChanged;
    }

    private void OnWaterStatusChanged(float newValue)
    {
        if (newValue >= _waterReservoir.GetMaxCapacity())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
