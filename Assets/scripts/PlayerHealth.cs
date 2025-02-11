using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibi� " + damage + " de da�o. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Aqu� podr�as agregar l�gica para reiniciar el nivel o mostrar una pantalla de Game Over
    }
}
