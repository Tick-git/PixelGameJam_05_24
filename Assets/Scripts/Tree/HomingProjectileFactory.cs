using UnityEngine;

[CreateAssetMenu(menuName = "Factories/HomingProjectileFactory")]
public class HomingProjectileFactory : ProjectileFactory
{
    public override GameObject CreateProjectile(Transform parent)
    {
        return Instantiate(_projectilePrefab, _spawnPosition, Quaternion.identity, parent);
    }
}
