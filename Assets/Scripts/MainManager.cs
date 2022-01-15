using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public string bestPlayerName;
    public int bestPlayerScore;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
  
    private bool m_GameOver = false;

    public static object Instance { get; internal set; }

    private string currentPlayerName;

    void Start()
    {
        currentPlayerName = "";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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
        if (SaveInstanseManager.Instance != null)
        {
            currentPlayerName = SaveInstanseManager.Instance.currentPlayerName;
        } else
        {
            currentPlayerName = "Unknown";
        }
        ScoreText.text = currentPlayerName + $" Score : {m_Points}";
        bestScoreText.text = LoadBest();
    }

    private void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = currentPlayerName + $" Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveInstanseManager.Instance.playerScore = m_Points;
        if(bestPlayerScore< m_Points)
        {
            SaveInstanseManager.Instance.SaveBestScore(currentPlayerName , m_Points);
        }
    }

    public string LoadBest()
    {
        SaveInstanseManager.Instance.LoadBestPlayer();
        bestPlayerName = SaveInstanseManager.Instance.playerName;
        bestPlayerScore = SaveInstanseManager.Instance.playerScore;
        string best = "Best " + bestPlayerName + $" Score : {bestPlayerScore}";
        return best;
    }
    
}
