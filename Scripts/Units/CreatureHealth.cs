using UnityEngine;
using UnityEngine.UI;

public class CreatureHealth : MonoBehaviour
{
    public CreatureStats stats;

    [Header("Health Bar UI")]
    public Canvas healthBarCanvas;
    public RectTransform healthBarFill;

    private void Start()
    {
        if (stats == null)
        {
            stats = new CreatureStats();
        }

        // Initialize health bar
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        stats.currentHealth -= damage;
        stats.currentHealth = Mathf.Clamp(stats.currentHealth, 0, stats.maxHealth);

        // Update the health bar
        UpdateHealthBar();

        if (stats.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // You can add other logic here if needed, e.g., playing a death animation or spawning a particle effect.

        Destroy(gameObject);
    }


    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float healthPercentage = stats.currentHealth / stats.maxHealth;
            healthBarFill.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
    }

    void Update()
    {
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(Camera.main.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0); // Adjust because it's initially backward-facing
        }
    }
}
