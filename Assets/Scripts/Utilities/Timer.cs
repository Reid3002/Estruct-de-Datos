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
    private LevelsManager levelsManager;
    // Start is called before the first frame update

    private void Awake()
    {
        levelsManager = GameObject.Find("LevelManager").GetComponent<LevelsManager>();

    }
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
                currentTime -= 1;
                timeAsText = currentTime.ToString();
                screenTimer.text = timeAsText;
                seconds = 60;
            }
            else
            {
                seconds--;
            }
        }
        else 
        {
            levelsManager.score = gameObject.GetComponent<GameManager>().points;
            levelsManager.active = true;            
        }
        
        
    }

    public void AddSeconds(int seconds)
    {
        currentTime += seconds;
    }
}
