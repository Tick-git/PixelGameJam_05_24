using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] FloatEventChannel _onPlayerHealthChanged;

    float _currentHealth = 100;

    PlayerSoundSystem _playerSoundSystem;

    private void Awake()
    {
        _playerSoundSystem = GetComponent<PlayerSoundSystem>();
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        _currentHealth -= damage;

        _onPlayerHealthChanged?.Invoke(_currentHealth);

        _playerSoundSystem.PlayPlayerIsHitSound();
    }

    public float GetMaxHealth()
    {
        return 100;
    }
}
