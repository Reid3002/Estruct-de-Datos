using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float startingTime;
    public float currentTime;
    [SerializeField] Text screenTimer;
    private string timeAsText;
    private float seconds = 60;
    [SerializeField] LevelsManager levelsManager;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        timeAsText = startingTime.ToString();
        screenTimer.text = timeAsText;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            if (seconds == 0)
            {
                currentTime -= 0.1f;
                timeAsText = currentTime.ToString();
                screenTimer.text = timeAsText;
                seconds = 60;
            }
            else
            {
                seconds--;
            }
        }
        else { levelsManager.active = true; }
        
        
    }

    public void AddSeconds(int seconds)
    {
        currentTime += seconds;
    }
}
