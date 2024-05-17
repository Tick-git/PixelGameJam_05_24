using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    float _currentHealth = 100;

    public Action<float> OnHealthChanged;

    PlayerSoundSystem _playerSoundSystem;

    private void Awake()
    {
        _playerSoundSystem = GetComponent<PlayerSoundSystem>();
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        _currentHealth -= damage;

        OnHealthChanged(_currentHealth);

        _playerSoundSystem.PlayPlayerIsHitSound();

        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float GetMaxHealth()
    {
        return 100;
    }

}
