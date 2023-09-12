using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EdibleQueue edibleScript;
    public Points _points;
    public float points;

    public int victoryScene = 0;
    public int loseScene = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        this.edibleScript = GetComponent<EdibleQueue>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.edibleScript.InitializeQueue();
        this.edibleScript.NextEdible();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(loseScene);
        }

        if (_points.points >= 100)
        {
            SceneManager.LoadScene(victoryScene);
        }


    }

    public void AddPoints(float quantity)
    {
        _points.AddPoints(quantity);
    }

    public void OnEdibleEated(EdibleController reference)
    {
        reference.OnEatedEvent -= OnEdibleEated;
        AddPoints(reference.pointQuantity);
        this.edibleScript.NextEdible();
    }

}
