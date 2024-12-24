using UnityEngine;
using System.IO;
using UnityEngine.UI; 
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    public Text scoreboardText;

    void Start()
    {
        LoadScores();  
    }

    void LoadScores()
    {
        
        string scoreFilePath = Path.Combine(Application.persistentDataPath, "score.txt");

        if (File.Exists(scoreFilePath))
        {
            
            string[] scoreLines = File.ReadAllLines(scoreFilePath);

            
            List<(int score, string line)> scoreList = new List<(int, string)>();

            foreach (string line in scoreLines)
            {
                
                int scoreStartIndex = line.IndexOf("Score: ") + 7;
                int scoreEndIndex = line.IndexOf(" at ");
                if (scoreStartIndex > 6 && scoreEndIndex > scoreStartIndex)
                {
                    string scoreStr = line.Substring(scoreStartIndex, scoreEndIndex - scoreStartIndex);
                    if (int.TryParse(scoreStr, out int score))
                    {
                        scoreList.Add((score, line));
                    }
                }
            }

           
            scoreList.Sort((a, b) => b.score.CompareTo(a.score));

            
            string displayText = "High Scores:\n";
            for (int i = 0; i < scoreList.Count; i++)
            {
                displayText += $"{i + 1}. {scoreList[i].line}\n";
            }

    
            Debug.Log("Sorted scores:\n" + displayText);

            
            scoreboardText.text = displayText;
        }
        else
        {
            Debug.Log("Score file not found at: " + scoreFilePath);
            scoreboardText.text = "No scores available.";
        }
    }
}
