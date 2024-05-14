using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 3.0f;

    private void Awake()
    {
        _speed = Random.Range(_speed - 0.4f, _speed + 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(Attack(collision.transform));
        }
    }

    private IEnumerator Attack(Transform target)
    {
        Vector3 initialOffset = new Vector2(0, Random.Range(-3, 3f));
        Vector3 currentOffset = initialOffset;
        
        while(true)
        {
            Vector2 nextTargetPosition = target.position + currentOffset;

            float distance = Vector2.Distance(transform.position, nextTargetPosition);

            if(distance < 1.5f)
            {
                currentOffset = Vector2.zero;
            } 
            else
            {
                currentOffset = initialOffset;
            }

            transform.position = Vector2.MoveTowards(transform.position, target.position + currentOffset, _speed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }
}
