using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Header("Score and Lives")]
    [SerializeField] int playerLives = 3;
     

    [Header("UI")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Scene")]
    [SerializeField] float loadDelay=2f;

    int score = 0;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = "Lives: "+playerLives.ToString();
        scoreText.text = "Score: "+score.ToString();
    }

    public void addToScore(int PointsToAdd)
    {
        score += PointsToAdd;
        scoreText.text = "Score: "+score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            Invoke("TakeLife", loadDelay);
        }
        else
        {
            //Load to Game Over Screen
            Invoke("ResetGameSession", loadDelay);
        }
    }
    public int  GetScore()
    {
        return score;
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = "Lives: "+playerLives.ToString();
    }
    
    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }     
}


