using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaterStatusDisplay : MonoBehaviour
{
    WaterReservoirBehavior _waterReservoir;
    Scrollbar _scrollbar;

    private void Awake()
    {
        _waterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<WaterReservoirBehavior>();
        _waterReservoir.OnWaterStatusChanged += OnWaterStatusChanged;

        _scrollbar = GetComponentInChildren<Scrollbar>();
    }

    private void OnDestroy()
    {
        _waterReservoir.OnWaterStatusChanged -= OnWaterStatusChanged;
    }

    private void OnWaterStatusChanged(float newValue)
    {
        _scrollbar.size = newValue / _waterReservoir.GetMaxCapacity();

        if (newValue >= _waterReservoir.GetMaxCapacity())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
