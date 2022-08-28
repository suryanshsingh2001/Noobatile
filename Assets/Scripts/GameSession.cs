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

    [SerializeField] int Gamescore = 0;

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
        //Remove this line and transfer to scorekeeper
        scoreText.text = "Score: "+Gamescore.ToString();
    }
    //Transfer to ScoreKeeper
    public void addToScore(int PointsToAdd)
    {
        Gamescore += PointsToAdd;
        scoreText.text = "Score: "+Gamescore.ToString();
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
            SceneManager.LoadScene(3);
        }
    }
    public void ResetGameScore()
    {
        Gamescore=0;
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
        FindObjectOfType<ScoreKeeper>().ResetShowScore();
        
        Destroy(gameObject);
    }     
}


