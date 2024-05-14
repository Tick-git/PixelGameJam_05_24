using System;
using System.Collections;
using UnityEngine;

public class DamageBehavior : MonoBehaviour, IDamageable 
{
    [SerializeField] float _knockbackForce = 10f;

    Rigidbody2D _rb;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();    
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        StopAllCoroutines();

        _rb.AddForce((transform.position - origin).normalized * _knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
        _rb.velocity = Vector2.zero;
    }
}
