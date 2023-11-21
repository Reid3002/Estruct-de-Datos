using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowScores : MonoBehaviour
{
    public int[] scores;
    private string[] names;
    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        scores = new int[EdibleScoreboard.EdibleScores.Count];
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = EdibleScoreboard.EdibleScores[i].score;
        }

        for (int i = 0; i < scores.Length; i++)
        {
            text.text += EdibleScoreboard.EdibleScores[i].type.ToString() + ": " + scores[i].ToString() + "\n";
        }
    }

    // Update is called once per frame

}
