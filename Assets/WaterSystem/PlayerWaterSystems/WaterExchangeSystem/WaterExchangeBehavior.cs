using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterExchangeBehavior : MonoBehaviour
{
    IWaterReservoir _playerWaterReservoir;

    Dictionary<int, WaterReservoirData> _waterReservoirsNearPlayer;

    PlayerStats _playerStats;

    float _waterExchangeCooldownTimer;

    private void Awake()
    {
        Transform rootTransform = TransformHelper.FindRootTransform(transform);
        _playerWaterReservoir = rootTransform.GetComponentInChildren<IWaterReservoir>();   
        _waterReservoirsNearPlayer = new Dictionary<int, WaterReservoirData>();
        _playerStats = rootTransform.GetComponentInChildren<IPlayerStats>().GetPlayerStats();
        _waterExchangeCooldownTimer = _playerStats.WaterExchangeCooldown;
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
        _waterExchangeCooldownTimer += Time.deltaTime / _playerStats.WaterExchangeCooldown;       

        if(_waterExchangeCooldownTimer > 1 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _waterReservoirsNearPlayer.Count > 0)
        {
            _waterReservoirsNearPlayer.Values.First().WaterReservoir.SetWater(_playerWaterReservoir.GetWater(1));
            _waterExchangeCooldownTimer = 0;
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
