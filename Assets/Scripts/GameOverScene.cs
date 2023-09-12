using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text playerLastScore;
    private int lastScore;
    public TMP_Text[] playerNameTexts;
    public TMP_Text[] scoreTexts;

    void Start()
    {
        lastScore = GameManager.Instance.lastPoints;
        playerLastScore.text = lastScore.ToString();
        // Assign references to the Text components in the Inspector or by using GameObject.Find
        playerNameTexts = new TMP_Text[5]; // Assuming you have 5 player name Text components
        scoreTexts = new TMP_Text[5]; // Assuming you have 5 score Text components

        for (int i = 0; i < 5; i++)
        {
            playerNameTexts[i] = GameObject.Find("PlayerNameText" + (i+1)).GetComponent<TMP_Text>();
            scoreTexts[i] = GameObject.Find("ScoreText" +( i+1)).GetComponent<TMP_Text>();
        }

        // Populate the table with scores and player names
        //LoadAndDisplayScores();
        PopulateTable();
    }

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void NewGame()
    {
        SceneManager.LoadScene("main");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void PopulateTable()
    {
        for (int i = 1; i <= 5; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i, 0);
            string playerName = PlayerPrefs.GetString("PlayerName" + i, "N/A");

            playerNameTexts[i - 1].text = playerName;
            scoreTexts[i - 1].text = score.ToString();
            PlayerPrefs.Save();

        }
    }
    void LoadAndDisplayScores()
    {
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + (i + 1), 0);
            string playerName = PlayerPrefs.GetString("PlayerName" + (i + 1), "N/A");

            playerNameTexts[i].text = playerName;
            scoreTexts[i].text = score.ToString();
        }
    }
    public void ClearScores()
    {
        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.DeleteKey("Score" + i);
            PlayerPrefs.DeleteKey("PlayerName" + i);
        }

        // After clearing, reload and display the scores (if needed)
        LoadAndDisplayScores();
    }
}
