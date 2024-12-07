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
        // Ha az instance null, akkor beállítjuk ezt az objektumot az instance-ra
        if (instance == null)
        {
            instance = this;

            // Csak alkalmazzuk a DontDestroyOnLoad-ot, ha nem az EndScene-rõl van szó
            if (SceneManager.GetActiveScene().name != "EndScene")
            {
                DontDestroyOnLoad(gameObject);  // Ne törlõdjön, amikor jelenetet váltunk
            }

            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);  // Ha már létezik másik példány, töröljük ezt
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndScene")
        {
            // Töröljük a Level objektumot és annak minden komponensét
            Destroy(instance.gameObject);  // A Level objektumot töröljük
            instance = null;  // Nullázzuk az instance-t, hogy újraindulhasson
        }
    }



    // Start is called before the first frame update
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
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Eseménykezelõ eltávolítása
    }




}
