using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static GameManager Instance;
    public float _points;
    public float points;

    public int victoryScene = 0;
    public int loseScene = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerController>().alive)
        {            
            Debug.Log("You lose");
            SceneManager.LoadScene(loseScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(loseScene);
        }

        if (_points >= 100)
        {
            SceneManager.LoadScene(victoryScene);
        }


    }


}
