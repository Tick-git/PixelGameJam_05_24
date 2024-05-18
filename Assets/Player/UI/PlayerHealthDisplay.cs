using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    PlayerHealthSystem _playerHealthSystem;
    Slider _healthBar;

    private void Awake()
    {
        _playerHealthSystem = TransformHelper.FindRootTransform(transform).GetComponentInChildren<PlayerHealthSystem>();
        _playerHealthSystem.OnHealthChanged += UpdateHealthDisplay;
        _healthBar = GetComponentInChildren<Slider>();
        _healthBar.value = 1;
    }

    private void OnDestroy()
    {
        _playerHealthSystem.OnHealthChanged -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(float currentHealth)
    {
        _healthBar.value = currentHealth / _playerHealthSystem.GetMaxHealth();
    }
}
