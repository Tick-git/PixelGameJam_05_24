using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Color _startColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        SetPlayerActiveStatus(false);

        StartCoroutine(EnableSteeringBehavior());
    }

    private void SetPlayerActiveStatus(bool isActive)
    {
        GetComponent<SteeringBehaviorBase>().enabled = isActive;
        GetComponent<EnemyHealthSystem>().enabled = isActive;
        GetComponent<EnemyDamageSystem>().enabled = isActive;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _spriteRenderer.color = _startColor;
    }

    private IEnumerator EnableSteeringBehavior()
    {
        Color tranparentColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
        float time = 0;

        while(time < 1)
        {
            _spriteRenderer.color = Color.Lerp(tranparentColor, _startColor, time);

            time += Time.deltaTime;
            yield return null;
        }

        _spriteRenderer.color = _startColor;
        SetPlayerActiveStatus(true);
    }
}
