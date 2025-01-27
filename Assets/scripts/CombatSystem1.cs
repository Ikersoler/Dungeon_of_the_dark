using UnityEngine;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
    [Header("Bar Settings")]
    public Slider precisionBar;
    public float barSpeed = 2f;

    private bool movingRight = true;
    private bool isCombatActive = false;

    [Header("Combat Settings")]
    public KeyCode actionKey = KeyCode.Space;
    public float perfectZoneMin = 0.45f;
    public float perfectZoneMax = 0.55f;

    [Header("UI Settings")]
    public GameObject combatUI; // UI del combate
    public GameObject basicUI;  // UI básica
    public Button attackButton; // Botón para atacar
    public Button fleeButton;   // Botón para huir

    private Enemy currentEnemy;

    private void Start()
    {
        if (precisionBar != null)
        {
            precisionBar.value = 0f;
        }

        SwitchToBasicUI();

        // Vincular botones a métodos
        if (attackButton != null) attackButton.onClick.AddListener(Attack);
        if (fleeButton != null) fleeButton.onClick.AddListener(Flee);
    }

    private void Update()
    {
        if (isCombatActive)
        {
            MoveBar();

            if (Input.GetKeyDown(actionKey))
            {
                CheckHit();
            }
        }
    }

    private void MoveBar()
    {
        if (precisionBar == null) return;

        if (movingRight)
        {
            precisionBar.value += Time.deltaTime * barSpeed;
            if (precisionBar.value >= 1f)
            {
                movingRight = false;
            }
        }
        else
        {
            precisionBar.value -= Time.deltaTime * barSpeed;
            if (precisionBar.value <= 0f)
            {
                movingRight = true;
            }
        }
    }

    private void CheckHit()
    {
        if (currentEnemy == null) return;

        if (precisionBar.value >= perfectZoneMin && precisionBar.value <= perfectZoneMax)
        {
            Debug.Log("Perfect Hit!");
            currentEnemy.TakeDamage(50); // Daño al enemigo
        }
        else
        {
            Debug.Log("Missed!");
        }
    }

    public void StartCombat(Enemy enemy)
    {
        currentEnemy = enemy;
        isCombatActive = true;
        if (precisionBar != null) precisionBar.value = 0f;
    }

    public void EndCombat()
    {
        isCombatActive = false;
        currentEnemy = null;
    }

    private void SwitchToCombatUI()
    {
        if (combatUI != null) combatUI.SetActive(true);
        if (basicUI != null) basicUI.SetActive(false);
    }

    private void SwitchToBasicUI()
    {
        if (combatUI != null) combatUI.SetActive(false);
        if (basicUI != null) basicUI.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el juego
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el juego
    }

    // Métodos vinculados a los botones de la UI
    private void Attack()
    {
        Debug.Log("Atacando al enemigo...");
        CheckHit();
    }

    private void Flee()
    {
        Debug.Log("El jugador ha huido del combate.");
        EndCombat();
    }
}
