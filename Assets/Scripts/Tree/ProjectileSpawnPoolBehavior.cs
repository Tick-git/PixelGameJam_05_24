using UnityEngine;

public class ProjectileSpawnPoolBehavior : MonoBehaviour
{
    [SerializeField] ProjectileFactory _projectileFactory;

    ProjectGameObjectPool _projectilePool;

    private void Awake()
    {
        _projectilePool = new ProjectGameObjectPool(_projectileFactory, 100);
        _projectilePool.InstantiateGameObjects();
    }

    public GameObject SpawnProjectile(Vector2 position)
    {
        GameObject projectile = _projectilePool.Get();

        projectile.transform.position = position;

        return projectile;
    }

    internal void DespawnProjectile(GameObject gameObject)
    {
        _projectilePool.Release(gameObject);
    }
}
