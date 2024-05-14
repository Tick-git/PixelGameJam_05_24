using UnityEngine;

public abstract class Steering : MonoBehaviour
{
    [SerializeField] int _weight;

    public int GetWeight()
    {
        return _weight;
    }

    public abstract SteeringData GetSteering(SteeringBehaviorBase steeringbase);
}
