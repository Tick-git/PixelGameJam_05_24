using System.Collections;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SetPlayerActiveStatus(false);

        StartCoroutine(StartSpawnSequence());
    }

    private void SetPlayerActiveStatus(bool isActive)
    {
        GetComponent<SteeringBehaviorBase>().enabled = isActive;
        GetComponent<EnemyHealthSystem>().enabled = isActive;
        GetComponent<EnemyDamageSystem>().enabled = isActive;
        GetComponent<Collider2D>().enabled = isActive;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartSpawnSequence()
    {
        GetComponent<Animator>().SetTrigger("Spawn");

        yield return new WaitForSeconds(1f);

        SetPlayerActiveStatus(true);
    }
}
