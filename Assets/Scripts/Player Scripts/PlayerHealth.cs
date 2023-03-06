using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI deadCount;
    [SerializeField] private GameObject deadMenu;
    [SerializeField] private int currentHealth;

    public float healthProgress;
    public int maxHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthProgress = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = healthProgress;
    }

    void Die()
    {
        deadMenu.SetActive(true);
        deadCount.text = "Убито врагов:  " + GetComponent<PlayerAttack>().deadEnemyCount.ToString();
    }
}