using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{
    public CombatSystem combatSystem;

    public GameObject pausePanel;


    private void Start()
    {
        HidePause();
        
    }

    private void Update()
    {
        //con esto oculto y muestro el inventario

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            HidePause();
        }
   
    }


    private void ShowPause()
    {
      combatSystem.combatUI.SetActive(false);
      combatSystem.basicUI.SetActive(false);
      pausePanel.SetActive(true);
    }


    private void HidePause()
    {
        combatSystem.combatUI.SetActive(false);
        combatSystem.basicUI.SetActive(false);
        pausePanel.SetActive(false);
    }
















    public void OnAttackButton()
    {
        combatSystem.Attack();
    }

    public void OnFleeButton()
    {
        combatSystem.Flee();
    }
}