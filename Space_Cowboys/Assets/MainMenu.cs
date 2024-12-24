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
        EditorApplication.isPlaying = false; // Editorben állítsa le a lejátszást
#else
        Application.Quit(); // Buildelt verzióban zárja be az alkalmazást
#endif
    }
}
