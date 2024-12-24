using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void ShowScores()
    {
        SceneManager.LoadScene("ScoreboardScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Editorben �ll�tsa le a lej�tsz�st
#else
        Application.Quit(); // Buildelt verzi�ban z�rja be az alkalmaz�st
#endif
    }
}
