using UnityEngine;

internal interface IDamageable
{
    void TakeDamage(int damage, Vector3 origin);
}