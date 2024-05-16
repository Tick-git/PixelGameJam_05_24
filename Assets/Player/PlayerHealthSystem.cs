using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    float _currentHealth = 100;

    Coroutine _playerCantBeHit;
    WaitForSeconds _playerHitCooldown;

    private void Awake()
    {
        _playerHitCooldown = new WaitForSeconds(0.25f);
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        if (_playerCantBeHit != null) return;

        _currentHealth -= damage;

        if(_currentHealth >= 0)
        {
            Debug.Log(_currentHealth);
        }

        _playerCantBeHit = StartCoroutine(ResetPlayerHitCooldown());
    }

    private IEnumerator ResetPlayerHitCooldown()
    {
        yield return _playerHitCooldown;

        _playerCantBeHit = null;
    }
}
