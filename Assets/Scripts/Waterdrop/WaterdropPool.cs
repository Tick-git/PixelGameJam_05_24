using UnityEngine;

public class WaterdropPool : GameObjectPool
{
    IWaterDropFactory _waterdropFactory;
    Transform _waterdropParentTransform;

    public WaterdropPool(IWaterDropFactory waterdropFactory, int preInstantiationCount) : base(preInstantiationCount)
    {
        _waterdropFactory = waterdropFactory;
        _waterdropParentTransform = new GameObject("Waterdrops").transform;
    }

    protected override GameObject CreateObject()
    {
        return _waterdropFactory.CreateWaterdrop(_waterdropParentTransform);
    }
}
