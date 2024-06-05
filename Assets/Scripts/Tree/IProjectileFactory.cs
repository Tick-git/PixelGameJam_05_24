using UnityEngine;

public interface IProjectileFactory
{
    GameObject CreateProjectile(Transform parent);
}