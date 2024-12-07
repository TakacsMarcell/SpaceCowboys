using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Level : MonoBehaviour
{

    public static Level instance;
    private Ship playerShip;
    int numDestructables= 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2", "Level3" };
    int currentLevel = 1;

    int score = 0;
    Text scoreText;

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
          
            Destroy(instance.gameObject);  
            instance = null;  
        }
    }



   
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
     if(startNextLevel)
        {
            if(nextLevelTimer <= 0)
            {
                currentLevel++;
                if(currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else {
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
        scoreText.text = score.ToString();
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if(numDestructables == 0)
        {
            startNextLevel = true;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }




}
