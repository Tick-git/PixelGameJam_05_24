using UnityEngine;

public abstract class ProjectileFactory : ScriptableObject, IProjectileFactory
{
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Vector3 _spawnPosition = new Vector2(-100, 100);

    public abstract GameObject CreateProjectile(Transform parent);
}
