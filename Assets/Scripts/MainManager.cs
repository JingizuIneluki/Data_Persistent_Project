using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public string playerName;
    public Text ScoreText;
    //public TMP_Text nameOfPlayer;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
   

    
    // Start is called before the first frame update
    void Start()
    {
        playerName= GameManager.Instance.playerNamePersistant;
        // nameOfPlayer.text = GameManager.Instance.playerNamePersistant;
        ScoreText.text = playerName+ $" Score : {m_Points}";
       
        BeginGame();
    }

    private void Update()
    {
        int numberOfBricks = FindObjectsOfType<Brick>().Length;
        if (!m_Started)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            GameManager.Instance.LastScore(playerName, m_Points);
            GameManager.AddNewScore(playerName, m_Points);
            SceneManager.LoadScene("TopScorers");
          
        }else if (numberOfBricks==0)
        {
            GameManager.Instance.AddVelocity(.3f);
            GameManager.Instance.AddVelocityPlayer(.3f);
            BeginGame();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
       // ScoreText.text = $" Score : {m_Points}";
        ScoreText.text = playerName + $" Score : {m_Points}";

    }

    public void GameOver()
    {
        m_GameOver = true;
       
    }
    private void BeginGame()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
