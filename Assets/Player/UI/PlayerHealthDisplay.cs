using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    PlayerHealthSystem _playerHealthSystem;
    Slider _healthBar;

    private void Awake()
    {
        _playerHealthSystem = TransformHelper.FindRootTransform(FindAnyObjectByType<PlayerController>().transform).GetComponentInChildren<PlayerHealthSystem>();
        _healthBar = GetComponentInChildren<Slider>();
        _healthBar.value = 1;
    }

    public void UpdateHealthDisplay(float currentHealth)
    {
        _healthBar.value = currentHealth / _playerHealthSystem.GetMaxHealth();
    }
}
