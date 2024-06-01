using UnityEngine;

[CreateAssetMenu(menuName = "Factories/WaterdropFactory")]
public class WaterdropFactory : ScriptableObject, IWaterDropFactory
{
    [SerializeField] GameObject _waterdropPrefab;
    [SerializeField] Vector3 _spawnPosition = new Vector2(-100, 100);

    public GameObject CreateWaterdrop(Transform parent)
    {
        return Instantiate(_waterdropPrefab, _spawnPosition, Quaternion.identity, parent);
    }
}
