using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
  
        SceneManager.LoadScene("Level1");
    }

    public void ShowScores()
    {
  
        Debug.Log("Scoreboard is under construction!");
    }

    public void ExitGame()
    {
       
        Debug.Log("Exiting Game...");

#if UNITY_EDITOR
       
        EditorApplication.isPlaying = false;
#else

#endif
    }
}