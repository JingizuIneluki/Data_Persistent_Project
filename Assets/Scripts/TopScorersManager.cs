using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TopScorersManager : MonoBehaviour
{
    public Text topScorersText;
    private const int maxTopScorers = 10;

    private List<ScoreData> topScorers = new List<ScoreData>();

    [Serializable]
    public class ScoreData
    {
        public string playerName;
        public int score;
    }

    private void Start()
    {
        LoadTopScorers();
        UpdateTopScorersText();
    }

    public void AddScore(string playerName, int score)
    {
        ScoreData newScore = new ScoreData { playerName = playerName, score = score };
        topScorers.Add(newScore);
        topScorers.Sort((a, b) => b.score.CompareTo(a.score)); // Sort in descending order

        if (topScorers.Count > maxTopScorers)
        {
            topScorers.RemoveAt(topScorers.Count - 1); // Remove the lowest score
        }

        SaveTopScorers();
        UpdateTopScorersText();
    }

    public List<ScoreData> GetTopScorers()
    {
        return topScorers;
    }

    private void UpdateTopScorersText()
    {
        topScorersText.text = "Top Scorers:\n";
        foreach (var scoreData in topScorers)
        {
            topScorersText.text += $"{scoreData.playerName}: {scoreData.score}\n";
        }
    }

    private void SaveTopScorers()
    {
        string jsonData = JsonUtility.ToJson(topScorers);
        PlayerPrefs.SetString("TopScorers", jsonData);
        PlayerPrefs.Save();
    }

    private void LoadTopScorers()
    {
        if (PlayerPrefs.HasKey("TopScorers"))
        {
            string jsonData = PlayerPrefs.GetString("TopScorers");
            topScorers = JsonUtility.FromJson<List<ScoreData>>(jsonData);
        }
    }
}
