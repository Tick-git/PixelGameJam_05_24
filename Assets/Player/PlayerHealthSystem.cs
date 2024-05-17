using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    float _currentHealth = 100;

    Coroutine _playerCantBeHit;
    WaitForSeconds _playerHitCooldown;

    public Action<float> OnHealthChanged;

    private void Awake()
    {
        _playerHitCooldown = new WaitForSeconds(0.125f);
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        if (_playerCantBeHit != null) return;

        _currentHealth -= damage;

        OnHealthChanged(_currentHealth);

        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        _playerCantBeHit = StartCoroutine(ResetPlayerHitCooldown());
    }

    public float GetMaxHealth()
    {
        return 100;
    }

    private IEnumerator ResetPlayerHitCooldown()
    {
        yield return _playerHitCooldown;

        _playerCantBeHit = null;
    }
}
