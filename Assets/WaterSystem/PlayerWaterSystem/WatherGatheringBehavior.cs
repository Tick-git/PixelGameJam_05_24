using UnityEngine;

public class WatherGatheringBehavior : MonoBehaviour
{
    IWaterReservoir _waterReservoir;

    private void Awake()
    {
        _waterReservoir = TransformHelper.FindRootTransform(transform).GetComponentInChildren<IWaterReservoir>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollisionIsWaterdrop(collision)) return;

        _waterReservoir.SetWater(collision.GetComponent<IWaterCollectable>().CollectWater(transform));
    }

    private bool CollisionIsWaterdrop(Collider2D collision)
    {
        return collision.CompareTag(GlobalVariables.WaterDropTag);
    }
}
