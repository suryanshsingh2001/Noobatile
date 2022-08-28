using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int Showscore = 0;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSinglton();
    }

    void ManageSinglton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetScore()
    {
        return Showscore;
    }
    public void ModifyScore(int value)
    {
        Showscore += value;
        Mathf.Clamp(Showscore, 0, int.MaxValue);
        Debug.Log(Showscore);
    }
    public void ResetShowScore()
    {
        Showscore = 0;
    }
}

