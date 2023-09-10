using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject firstMenu;
    public int playableScene;
   

    GameObject currentMenu;

    private void Start()
    {
        currentMenu = firstMenu;
    }


    public void GoScene()
    {
        SceneManager.LoadScene(playableScene);
    }
    public void Nivel1()
    {
        SceneManager.LoadScene(2);
    }
    public void Nivel2()
    {
        SceneManager.LoadScene(3);
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
    