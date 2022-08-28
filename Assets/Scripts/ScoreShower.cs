using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreShower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;


    GameSession gameSession;
    ScenePersist scenePersist;
    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        scenePersist = FindObjectOfType<ScenePersist>();

    }
    private void Start()
    {
        ShowScore();
    }

    private void ShowScore()
    {
     finalScore.text= "Your Score is: "+gameSession.GetScore().ToString();
    }
}
