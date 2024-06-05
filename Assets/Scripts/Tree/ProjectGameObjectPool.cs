using UnityEngine;

public class ProjectGameObjectPool : GameObjectPool
{
    IProjectileFactory _projectileFactory;
    Transform _parent;

    public ProjectGameObjectPool(IProjectileFactory projectileFactory, int preInstantiationCount) : base(preInstantiationCount)
    {
        _projectileFactory = projectileFactory;
        _parent = new GameObject("Projectiles").transform;
    }

    protected override GameObject CreateObject()
    {
        return _projectileFactory.CreateProjectile(_parent);
    }
}
