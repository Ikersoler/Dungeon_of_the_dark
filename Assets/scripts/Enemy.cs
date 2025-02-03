using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int health = 100;

    public delegate void EnemyDefeated(Enemy enemy);
    public static event EnemyDefeated OnEnemyDefeated;


    [Header("Combat Settings")]
    public CombatSystem combatSystem; // Referencia al sistema de combate

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que toca es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador tocó al enemigo: " + enemyName);
            combatSystem.StartCombat(this); // Inicia el combate con este enemigo
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(enemyName + " took " + amount + " damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(enemyName + " ha sido derrotado.");

        combatSystem.EndCombat(); // Asegurar que el combate finalice antes de destruir
        Destroy(gameObject); //  Se destruye el enemigo después de terminar el combate
    }
}
