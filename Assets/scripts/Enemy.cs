using UnityEngine;

using System.Collections;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int health = 100;

    public delegate void EnemyDefeated(Enemy enemy);
    public static event EnemyDefeated OnEnemyDefeated;

    public int attackDamage = 10;
    public float attackInterval = 2f; // Tiempo entre ataques

    [Header("Combat Settings")]
    public CombatSystem combatSystem; // Referencia al sistema de combat
    private PlayerHealth playerHealth;

    private UIManager uiManager;
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
           gameObject.SetActive(false);

        }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que toca es el jugador
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador tocó al enemigo: " + enemyName);
            combatSystem.StartCombat(this); // Inicia el combate con este enemigo
            
            StartCoroutine(AttackPlayer()); // Comienza a atacar al jugador
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
        
       
        combatSystem.EndCombat();
        if (gameObject.CompareTag("Boss"))
        {
            WinCondition();
        }
        Destroy(gameObject); //  Se destruye el enemigo después de terminar el combate
        Debug.Log(gameObject.name);
        
    }

    private IEnumerator AttackPlayer()
    {
        
        while (combatSystem.IsCombatActive() && playerHealth != null && !playerHealth.IsDead() )
        {
            Debug.Log($"condicion{attackInterval}");
            yield return new WaitForSeconds(attackInterval);
            Debug.Log(enemyName + " ataca al jugador e inflige " + attackDamage + " de daño.");
            playerHealth.TakeDamage(attackDamage);

        }
    }

    public void WinCondition()
    {
       uiManager.showWin();
       Time.timeScale = 0f;
    }



}
