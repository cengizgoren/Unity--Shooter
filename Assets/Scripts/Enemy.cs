using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController = null;

    [Header("UI")]
    [SerializeField] private Image healthBarImage = null;

    private void OnEnable()
    {
        UpdateHealthBar(enemyController.Health, enemyController.MaxHealth);

        enemyController.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        enemyController.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(object sender, EnemyController.HealthChangedEventArgs e)
    {
        UpdateHealthBar(e.Health, e.MaxHealth);
    }

    private void UpdateHealthBar(float health, float maxHealth)
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
