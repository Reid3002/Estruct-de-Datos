using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EdibleController;

public class ShowScores : MonoBehaviour
{
    public int[] scores;
    private string[] names;
    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        Dictionary<EdibleType, int> scores = new Dictionary<EdibleType, int>();

        for (int i = 0; i < EdibleScoreboard.EdibleScores.Count; i++)
            scores.Add(EdibleScoreboard.EdibleScores[i].type, EdibleScoreboard.EdibleScores[i].score);

        var sortedDict = from score in scores orderby score.Value descending select score;

        text.text += "\n";
        foreach (var score in sortedDict)
            text.text += score.Key + ": " + score.Value + "\n";
    }
}
