using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; 
    public Slider normalhealthBar; 
    [SerializeField] private UIManager uiManager;
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("Jugador recibió " + damage + " de daño. Vida restante: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        Time.timeScale = 0;
        uiManager.showGameOver();
        
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
        
    }

    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Vida actual: " + currentHealth);
        
    }
}
