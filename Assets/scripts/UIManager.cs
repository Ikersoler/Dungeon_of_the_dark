using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{
    public CombatSystem combatSystem;

    public GameObject pausePanel;

    public GameObject StartPanel;

  

    private void Start()
    {
        HidePause();

        if (StartPanel != null)
        {
            StartPanel.SetActive(true);
        }

        
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


    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void opciones()
    {
        SceneManager.LoadScene(2);
    }

    public void Game()
    {
        SceneManager.LoadScene(0);
    }

    public void Credits()
    {
        SceneManager.LoadScene(4);
    }

    public void exit()
    {
        Application.Quit();
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