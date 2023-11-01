using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject firstMenu;


    public int mainMenuScene;
    public int levelOne;
    public int levelTwo;


    GameObject currentMenu;

    private void Start()
    {
        currentMenu = firstMenu;
    }


    public void GoScene()
    {
        SceneManager.LoadScene(levelOne);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void Nivel1()
    {
        SceneManager.LoadScene(levelOne);
    }
    public void Nivel2()
    {
        SceneManager.LoadScene(levelTwo);
    }
    public void Exit()
    {
        Application.Quit();
    }

    

    public void GoTo(GameObject panelToGo)
    {
        panelToGo.SetActive(true);
        currentMenu.SetActive(false);
        currentMenu = panelToGo;
    }
}
    