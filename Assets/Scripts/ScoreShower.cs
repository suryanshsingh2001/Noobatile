using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreShower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;

    ScoreKeeper scoreKeeper;


    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }
    private void Start()
    {
        ShowScore();
    }

    private void ShowScore()
    {
     finalScore.text= "Your Score is: "+scoreKeeper.GetScore().ToString();
    }
}
