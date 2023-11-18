using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    public Text Puntos;
    public int Score;


    // Start is called before the first frame update
    void Start()
    {
    Score = (int)Scoreboard.Instance.Points;


    }

    // Update is called once per frame
    void Update()
    {

        Puntos.text = "Puntos: " + Score.ToString();

    }
}
