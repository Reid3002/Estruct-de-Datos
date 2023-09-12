using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EdibleController;

public class GameManager : MonoBehaviour
{
    [Header("Game status")]
    public float points;

    [Header("Settings")]
    public float pointsForWinScene = 500;
    public int victoryScene = 0;
    public int loseScene = 0;

    [Header("References")]
    public static GameManager Instance;
    public EdibleQueue edibleScript;
    public TextMeshProUGUI textMesh;
    public PlayerController thePlayer;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.edibleScript.edibleTypesAmount = this.edibleScript.edibleTypesAmount = System.Enum.GetNames(typeof(EdibleType)).Length;
        this.edibleScript.InitializeQueue();
        this.edibleScript.NextEdible();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(loseScene);
        }

        if (this.points >= pointsForWinScene)
        {
            SceneManager.LoadScene(victoryScene);
        }

        this.points += t;
        this.textMesh.text = points.ToString("0");

        this.thePlayer.PlayerUpdate(t);
    }

    public void AddPoints(float quantity)
    {
        this.points += quantity;
    }

    public void OnEdibleEated(EdibleController reference)
    {
        reference.OnEatedEvent -= OnEdibleEated;
        AddPoints(reference.pointQuantity);
        this.edibleScript.NextEdible();
    }

}
