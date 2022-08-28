using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<audioPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
