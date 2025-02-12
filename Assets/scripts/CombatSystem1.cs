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
    public GameObject combatUI;
    public GameObject basicUI;
    public Button attackButton;
    public Button fleeButton;

    private Enemy currentEnemy;

    private void Start()
    {
        if (precisionBar != null)
        {
            precisionBar.value = 0f;
        }

        //  Asegurar que la UI básica está activa y la UI de combate desactivada
        if (combatUI != null) combatUI.SetActive(false);
        if (basicUI != null) basicUI.SetActive(true);
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

        //  USAMOS Time.unscaledDeltaTime PARA QUE FUNCIONE DURANTE LA PAUSA
        float moveAmount = Time.unscaledDeltaTime * barSpeed;

        if (movingRight)
        {
            precisionBar.value += moveAmount;
            if (precisionBar.value >= 1f)
            {
                movingRight = false;
            }
        }
        else
        {
            precisionBar.value -= moveAmount;
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
            Debug.Log("¡Golpe Perfecto!");
            currentEnemy.TakeDamage(50);
        }
        else
        {
            Debug.Log("¡Fallaste!");
        }
    }

    public void StartCombat(Enemy enemy)
    {
        Debug.Log("¡Combate iniciado contra " + enemy.enemyName + "!");
        currentEnemy = enemy;
        isCombatActive = true;
        if (precisionBar != null) precisionBar.value = 0f;

        SwitchToCombatUI();
        PauseGame();
    }

    public void EndCombat()
    {
        Debug.Log("Combate terminado.");
        isCombatActive = false;
        currentEnemy = null;

        SwitchToBasicUI();
        ResumeGame();
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
/*
    private void Attack()
    {
        Debug.Log("Atacando...");
        CheckHit();
    }

    private void Flee()
    {
        Debug.Log("El jugador ha huido del combate.");
        EndCombat();
    }
    */
}