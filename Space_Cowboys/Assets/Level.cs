using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level instance;
    private Ship playerShip;
    int numDestructables = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2", "Level3" };
    int currentLevel = 1;

    int score = 0;
    Text scoreText;

 
    private string scoreFilePath = "scoreboard.txt";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (SceneManager.GetActiveScene().name != "EndScene")
            {
                DontDestroyOnLoad(gameObject);
            }

            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndScene")
        {
            SaveScore(score); 

            
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

            
            LoadScores();

            
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    void Start()
    {
       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else
                {
                    SceneManager.LoadScene("EndScene");
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    
    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = "Score: " + score.ToString(); 
    }

    
    public void AddDestructable()
    {
        numDestructables++;
    }

    
    public void RemoveDestructable()
    {
        numDestructables--;

        
        if (numDestructables == 0)
        {
            startNextLevel = true;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void SaveScore(int score)
    {
        
        string scoreText = "Score: " + score + " at " + System.DateTime.Now.ToString();
        
        string scoreFilePath = Path.Combine(Application.persistentDataPath, "score.txt");

        
        File.AppendAllText(scoreFilePath, scoreText + "\n");

        Debug.Log("Score saved to: " + scoreFilePath); 
    }

    
    private void LoadScores()
    {
        string scoreFilePath = Path.Combine(Application.persistentDataPath, "score.txt");

        if (File.Exists(scoreFilePath))
        {
            string[] allScores = File.ReadAllLines(scoreFilePath);

            string displayText = "High Scores:\n";
            foreach (string scoreLine in allScores)
            {
                displayText += scoreLine + "\n"; 
            }

            
            scoreText.text = displayText;
        }
        else
        {
            scoreText.text = "No scores available.";
        }
    }

    
    public int Score
    {
        get { return score; }
    }
}
