using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    public void Shoot(Transform target, int damage)
    {
        StartCoroutine(HandleShoot(target, damage));
    }

    private IEnumerator HandleShoot(Transform target, int damage)
    {
        while(Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }

        target.GetComponent<IDamageable>().TakeDamage(damage, transform.position);

        Destroy(gameObject);
    }
}