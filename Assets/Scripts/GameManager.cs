using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game status")]
    public float points;

    [Header("Game table")]
    public Vector2 minLocation = new Vector2(-8.5f, -5);
    public Vector2 maxLocation = new Vector2(9, 4);

    [Header("Settings")]
    public float pointsForWinScene = 500;
    public int victoryScene = 0;
    public int loseScene = 0;

    // yikes no toquen esto
    private Vector3 objectLocation = new Vector3(-7.033192f, 4.494306f, 1);

    [Header("References")]
    public PlayerController thePlayer;
    public static GameManager Instance;
    public TextMeshProUGUI textMesh;

    private EdibleQueue edibleScript;    
    private EdibleGlobalEffects edibleEffectsScript;
    private EnemyQueue enemyQueueScript;
    private PoolStack poolStackScript;

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
        this.transform.position = this.objectLocation;
        
        this.edibleScript = GetComponent<EdibleQueue>();
        this.edibleEffectsScript = GetComponent<EdibleGlobalEffects>();
        this.enemyQueueScript = GetComponent<EnemyQueue>();
        this.poolStackScript= GetComponent<PoolStack>();

        this.edibleScript.edibleTypesAmount = this.edibleScript.edibleTypesAmount = System.Enum.GetNames(typeof(EdibleController.EdibleType)).Length;
        this.edibleScript.InitializeQueue();
        this.edibleScript.NextEdible();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Escape) || !thePlayer.alive)
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
        Scoreboard.Instance.Points += t;
    }

    public void AddPoints(float quantity)
    {
        this.points += quantity;
        Scoreboard.Instance.Points += quantity;
    }

    public void OnEdibleEated(EdibleController reference)
    {
        reference.OnEatedEvent -= OnEdibleEated;
        AddPoints(reference.pointQuantity);
        this.edibleScript.NextEdible();

        switch (reference.type)
        {
            case EdibleController.EdibleType.AddTail:
                break;
            case EdibleController.EdibleType.RemoveTail:
                break;
            case EdibleController.EdibleType.SlowPlayer:
                break;
            case EdibleController.EdibleType.PlayerReverse:
                break;
            case EdibleController.EdibleType.StunEnemies:
                break;
            case EdibleController.EdibleType.LightsOff:
                this.edibleEffectsScript.ExecEffect(EdibleGlobalEffects.EffectType.LightsOff);
                break;
            case EdibleController.EdibleType.LightsOn:
                this.edibleEffectsScript.ExecEffect(EdibleGlobalEffects.EffectType.LightsOn);
                break;
            case EdibleController.EdibleType.KillEnemy:
                this.poolStackScript.Stack(this.enemyQueueScript.Dequeue());
                break;
            default:
                break;
        }
    }

}