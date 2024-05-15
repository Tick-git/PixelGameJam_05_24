using System.Collections.Generic;
using UnityEngine;

public class AoeSpellController : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    
    Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    internal void CastSpell(Vector2 position)
    {
        if (position.magnitude > 1.5f) return;

        List<Collider2D> colliders = new List<Collider2D>();

        transform.localPosition = position;
        _collider.enabled = true;
        _collider.OverlapCollider(new ContactFilter2D() { useTriggers = true, useLayerMask = true, layerMask = _layerMask }, colliders);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable enemy))
            {
                enemy.TakeDamage(10, transform.position);
            }
        }

        _collider.enabled = false;
    }
}
