using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CombatSystem combatSystem;
    public Enemy enemyPrefab;
    public Transform[] spawnPoints; // Cambiado a un array para m�ltiples puntos de aparici�n
    private Enemy currentEnemy;

    private void Start()
    {
        // Bucle para todos los spawn points
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemy(spawnPoint); // Llama a SpawnEnemy para cada punto de aparici�n
        }
    }

    public void SpawnEnemy(Transform position)
    {
        if (enemyPrefab == null || position == null || combatSystem == null)
        {
            Debug.LogError("Faltan referencias en el GameManager");
            return;
        }
        // Instancia al enemigo en la escena 
        currentEnemy = Instantiate(enemyPrefab, position.position, Quaternion.identity);
    }
    
}
