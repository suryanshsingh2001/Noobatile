using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Anouncer : MonoBehaviour
{
    
    [SerializeField] AudioClip[] levelSounds;


    AudioSource audioSource;

    
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        LevelAnnounce();
    }

    void LevelAnnounce()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < levelSounds.Length; i++)
        {
            audioSource.PlayOneShot(levelSounds[currentSceneIndex-1]);
           
        }
    }

}
