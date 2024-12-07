using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StoryScene : MonoBehaviour
{
    public string textFileName;
    public Text storyText;
    public string nextSceneName;
    public float displayDuration = 10f;

    private void Start()
    {
        LoadStoryText();
        Invoke("LoadNextScene", displayDuration);
    }

    void LoadStoryText()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, textFileName);
        if (File.Exists(filePath))
        {
            string storyContent = File.ReadAllText(filePath);
            storyText.text = storyContent;
        }
        else
        {
            storyText.text = "A történet szövege nem található!";
        }
    }

    void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
