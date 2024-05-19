using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    private static ProjectSpawnPoolBehavior _projectSpawnPoolBehavior;

    private void Awake()
    {
        if (_projectSpawnPoolBehavior == null)
        {
            _projectSpawnPoolBehavior = FindObjectOfType<ProjectSpawnPoolBehavior>();
        }
    }

    public void Shoot(Transform target, int damage)
    {
        StartCoroutine(HandleShoot(target, damage));
    }

    private IEnumerator HandleShoot(Transform target, int damage)
    {
        while(Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime) ;
            yield return null;
        }

        if (target.gameObject.activeSelf)
        {
            target.GetComponent<IDamageable>().TakeDamage(damage, transform.position);
        }

        _projectSpawnPoolBehavior.DespawnProjectile(gameObject);
    }
}