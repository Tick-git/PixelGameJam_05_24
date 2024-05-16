using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterExchangeBehavior : MonoBehaviour
{
    IWaterReservoir _playerWaterReservoir;

    Dictionary<int, WaterReservoirData> _waterReservoirsNearPlayer;

    private void Awake()
    {
        _playerWaterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<IWaterReservoir>();   
        _waterReservoirsNearPlayer = new Dictionary<int, WaterReservoirData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWaterReservoir waterReservoir = TransformHelper.FindRootTransform(collision.transform).GetComponentInChildren<IWaterReservoir>();

        if (waterReservoir != null)
        {
            _waterReservoirsNearPlayer.Add(collision.GetInstanceID(), new WaterReservoirData(collision.transform, waterReservoir));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_waterReservoirsNearPlayer.ContainsKey(collision.GetInstanceID()))
        {
            _waterReservoirsNearPlayer.Remove(collision.GetInstanceID());
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _waterReservoirsNearPlayer.Count > 0)
        {
            _waterReservoirsNearPlayer.Values.First().WaterReservoir.SetWater(_playerWaterReservoir.GetWater(10));
        }
    }

    private class WaterReservoirData
    {
        public Transform WaterReservoirPosition;
        public IWaterReservoir WaterReservoir;

        public WaterReservoirData(Transform transform, IWaterReservoir waterReservoir)
        {
            WaterReservoirPosition = transform;
            WaterReservoir = waterReservoir;
        }
    }
}
