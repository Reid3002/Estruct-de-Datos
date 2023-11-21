using System.Collections.Generic;
using UnityEngine;
using static EdibleController;

public static class EdibleScoreboard
{    
    public class EdibleScore
    {
        public EdibleType type;
        public int score;
    }

    private static List<EdibleScore> edibleScores = new List<EdibleScore>();
    public static List<EdibleScore> EdibleScores => edibleScores;

    public static void InitializeEdibleScoreboard()
    {
        edibleScores.Clear();
        int edibleList = 0;

        for (int i = 0; i < 8; i++)
        {
            switch (edibleList)
            {
                case 0: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.AddTail));          break;
                case 1: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.RemoveTail));       break;
                case 2: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.SlowPlayer));       break;
                case 3: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.PlayerReverse));    break;
                case 4: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.StunEnemies));      break;
                case 5: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.LightsOff));        break;
                case 6: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.LightsOn));         break;
                case 7: edibleScores.Add(GenerateEdibleScoreItem(EdibleType.KillEnemy));        break;
            }
            edibleList++;
        }
    }

    private static EdibleScore GenerateEdibleScoreItem(EdibleType type)
    {
        EdibleScore edible = new EdibleScore();
        edible.type = type;
        edible.score = 0;
        return edible;
    }

    public static void AddPointToEdible(EdibleType type)
    {
        foreach (EdibleScore edibleScore in edibleScores)
        {
            if (edibleScore.type == type)
                edibleScore.score++;
        }
    }

    public static void SortScores()
    {
        List<EdibleScore> sortedEdibleScores = EdibleScores;
        QuickSort.QuickSortEdibleScore(sortedEdibleScores, 0, sortedEdibleScores.Count - 1);
        
        string text = "Current edible scores:\n";
        foreach (EdibleScore edibleScore in sortedEdibleScores)
            text = text + edibleScore.type + ": " + edibleScore.score + "\n";

        text = text + "End.";
        Debug.LogWarning(text);
    }
}
