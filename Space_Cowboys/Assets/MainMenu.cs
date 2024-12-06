using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Az els� szint bet�lt�se
        SceneManager.LoadScene("Level1");
    }

    public void ShowScores()
    {
        // Ideiglenes pontsz�m ki�r�s
        Debug.Log("Scoreboard is under construction!");
    }

    public void ExitGame()
    {
        // Kil�p�s a j�t�kb�l
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}