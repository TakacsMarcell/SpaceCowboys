using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Az elsõ szint betöltése
        SceneManager.LoadScene("Level1");
    }

    public void ShowScores()
    {
        // Ideiglenes pontszám kiírás
        Debug.Log("Scoreboard is under construction!");
    }

    public void ExitGame()
    {
        // Kilépés a játékból
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}