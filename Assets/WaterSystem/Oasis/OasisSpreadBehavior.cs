using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OasisSpreadBehavior : MonoBehaviour
{
    Dictionary<int, GameObject> _oasisExtensions;
    WaterReservoirBehavior _oasisWaterReservoir;

    int _currentExtensionLayer;
    int _oasisExtendedCount;

    private void Awake()
    {
        FindAllOasisExtensions();

        _oasisWaterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<WaterReservoirBehavior>();
        _oasisWaterReservoir.OnWaterStatusChanged += OnWaterStatusChanged;
        _oasisExtendedCount = 0;
    }

    private void OnDestroy()
    {
        _oasisWaterReservoir.OnWaterStatusChanged -= OnWaterStatusChanged;
    }

    private void OnWaterStatusChanged(float newStatus)
    {
        int nextExtensionLayer = _currentExtensionLayer + 1;
        float threshold = _oasisWaterReservoir.GetMaxCapacity() / _oasisExtensions.Count;

        if(newStatus >= threshold * nextExtensionLayer)
        {
            _oasisExtensions[nextExtensionLayer].SetActive(true);
            _oasisExtensions[_currentExtensionLayer].SetActive(false);
            _currentExtensionLayer = nextExtensionLayer;  
            _oasisExtendedCount++;
        }       

        if(_oasisExtendedCount >= _oasisExtensions.Count - 1)
        {
            Destroy(gameObject);
        }
    }

    private void FindAllOasisExtensions()
    {
        _oasisExtensions = new Dictionary<int, GameObject>();
        List<OasisExtension> oasisExtensions = FindObjectsOfType<OasisExtension>().ToList();

        foreach (OasisExtension extension in oasisExtensions)
        {
            if (!_oasisExtensions.ContainsKey(extension.ExtensionLayer))
            {
                _oasisExtensions[extension.ExtensionLayer] = extension.gameObject;
            }
            else
            {
                throw new System.Exception($"Oasis Extension layer already exists on {extension.gameObject.name}");
            }
        }
    }
}
