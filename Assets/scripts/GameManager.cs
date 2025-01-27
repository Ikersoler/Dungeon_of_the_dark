using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CombatSystem combatSystem; // Referencia al sistema de combate
    public Enemy enemyPrefab;         // Prefab del enemigo
    public Transform spawnPoint;      // Punto de aparición del enemigo

    private Enemy currentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    // Método para crear y configurar un nuevo enemigo
    public void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null || combatSystem == null)
        {
            Debug.LogError("Faltan referencias en el GameManager");
            return;
        }

        // Instancia al enemigo en la escena
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        combatSystem.StartCombat(currentEnemy); // Inicia el combate con el nuevo enemigo
    }

    // Este método puede ser llamado por el evento del enemigo al morir
    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= HandleEnemyDefeated;
    }

    private void HandleEnemyDefeated(Enemy defeatedEnemy)
    {
        if (defeatedEnemy == currentEnemy)
        {
            Debug.Log("Enemigo derrotado. Preparando el siguiente combate...");
            combatSystem.EndCombat();

            // Espera un momento antes de crear un nuevo enemigo (simulación de transición)
            Invoke(nameof(SpawnEnemy), 2f); // Espera 2 segundos
        }
    }
}
