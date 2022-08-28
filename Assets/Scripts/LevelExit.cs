using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] float levelLoadDelay = 1f;

    [Header("Finish Effects")]
    [SerializeField] AudioClip finishSound;
    [SerializeField] [Range(0f,1f)] float finishFXVolume=0.75f;
   


    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource.PlayOneShot(finishSound,finishFXVolume);
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex= currentSceneIndex + 1;
        
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //Go to Game Over Menu
            nextSceneIndex = 0; 
            
        }
     
        
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }






}
