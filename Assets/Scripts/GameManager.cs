using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lastPoints;
    public string playerNamePersistant;
    public Color TeamColor=Color.white;
    private AudioSource music;
    public  int musicOnOff;
    public float ballMaxVelocity = 3f;
    public float speedPlayer;
   


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;

        }
        Instance = this;
      

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        music = GetComponent<AudioSource>();
        TeamColor = new Color(PlayerPrefs.GetFloat("RedValue"), PlayerPrefs.GetFloat("GreenValue"), PlayerPrefs.GetFloat("BlueValue"));
        int musicPreferences = PlayerPrefs.GetInt("Music");
        if (musicPreferences == 0)
        {
            music.Play();
        }
        else if (musicPreferences == 1)
        {
            music.Stop();
        }
        speedPlayer = 2.5f;
        ballMaxVelocity = 3f;
        InitializeTopScores();
    }
    private void Update()
    {
        
    }
    public void StartStopMusic(int musicOnOff)
    {

        if (musicOnOff == 0)
        {
            music.Play();
        }
        else if (musicOnOff == 1)
        {
            music.Stop();
        }
        SaveMusicPreferences(musicOnOff);
    }
    public void SetColorValues(float redColor, float greenColor, float blueColor)
    {
        PlayerPrefs.SetFloat("RedValue", redColor);
        PlayerPrefs.SetFloat("GreenValue", greenColor);
        PlayerPrefs.SetFloat("BlueValue", blueColor);
        
    }
    public void SaveNamePlayer(string playerName)
    {
        playerNamePersistant = playerName;
    }
    private void SaveMusicPreferences(int preferences)
    {
        PlayerPrefs.SetInt("Music", preferences);
    }
    public float AddVelocity( float addVelocityBall)
    {
        ballMaxVelocity += addVelocityBall;
        
        
        return ballMaxVelocity; 
      
    }
    public float AddVelocityPlayer(float addVelocityPlayer)
    {
        speedPlayer += addVelocityPlayer;


        return speedPlayer;

    }
    public void LastScore(string name,int score) {

        lastPoints = score;
        GetNewScorePosition(score);
    }
    public static int GetNewScorePosition(int newScore)
    {
        int newPosition = -1; // Default position if the score doesn't make it into the top 5

        // Retrieve saved scores and compare with the new score
        for (int i = 1; i <= 5; i++)
        {
            int savedScore = PlayerPrefs.GetInt("Score" + i, 0); // 0 is the default value if the key doesn't exist

            if (newScore >= savedScore)
            {
                newPosition = i; // The new score is greater or equal to this position
                break; // Stop checking, as we found the position
            }
        }

        return newPosition;
    }
    void InitializeTopScores()
    {
        for (int i = 1; i <= 5; i++)
        {
            // Check if the PlayerPrefs keys already exist
            if (!PlayerPrefs.HasKey("Score" + i))
            {
                PlayerPrefs.SetInt("Score" + i, 0); // Set to 0 or any initial value
            }

            if (!PlayerPrefs.HasKey("PlayerName" + i))
            {
                PlayerPrefs.SetString("PlayerName" + i, "Player" + i); // Set to "Player1," "Player2," etc.
            }
        }
    }


    public static void AddNewScore(string playerName, int newScore)
    {
        int newPosition = GetNewScorePosition(newScore);

        if (newPosition >= 0)
        {
            // Shift existing scores and player names to make room for the new score
            for (int i = 5; i > newPosition; i--)
            {
                int prevScore = PlayerPrefs.GetInt("Score" + (i - 1), 0);
                string prevName = PlayerPrefs.GetString("PlayerName" + (i - 1), "");

                PlayerPrefs.SetInt("Score" + i, prevScore);
                PlayerPrefs.SetString("PlayerName" + i, prevName);
            }

            // Insert the new score and player name at the newPosition
            PlayerPrefs.SetInt("Score" + newPosition, newScore);
            PlayerPrefs.SetString("PlayerName" + newPosition, playerName);
            PlayerPrefs.Save();
            
        }
    }

   

}
