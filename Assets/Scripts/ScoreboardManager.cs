using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText; // Reference to the Text component to display the scoreboard
    private List<ScoreData> scores = new List<ScoreData>(); // List to store the scores
    private string filePath; // Path to the XML file



    void Start()
    {
        scoreboardText = GetComponent<TextMeshProUGUI>(); // Assign the Text component if not set in the inspector
        filePath = Application.persistentDataPath + "/scores.xml";
        Debug.Log(Application.persistentDataPath);
        LoadScores();
        UpdateScoreboard();
    }

    public void AddScore( int score,string grade_str)
    {
        ScoreData newScore = new ScoreData { score = score, grade_str = grade_str};
        scores.Add(newScore);
        SaveScores();
        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        if (scoreboardText == null)
        {
           // Debug.LogError("Scoreboard Text reference is not set!");
            return;
        }

        string scoreboard = "Scoreboard\n\n"+"Rank\t\tScore\t\tGrade\n";;
         for (int i = 0; i < scores.Count; i++)
    {
          scoreboard += (i + 1) + "\t\t" + scores[i].score + "\t\t" + scores[i].grade_str + "\n";
    }
        scoreboardText.text = scoreboard;
    }

    public void SaveScores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<ScoreData>));
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fileStream, scores);
        }
    }

    public List<ScoreData> LoadScores()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ScoreData>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                scores = (List<ScoreData>)serializer.Deserialize(fileStream);
            }

            // Sort scores based on the score value
            scores = scores.OrderByDescending(s => s.score).Take(8).ToList();
        }
        return scores;
    }
}