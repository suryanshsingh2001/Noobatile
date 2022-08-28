using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] float delay=1f;
  

    public void LoadGame()
    {

        SceneManager.LoadScene("Level 1");
        FindObjectOfType<ScoreKeeper>().ResetShowScore();
        FindObjectOfType<GameSession>().ResetGameSession();
    }
    public void LoadMainMenu()
    {
        StartCoroutine(WaitandLoad("MainMenu", delay));
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitandLoad("GameOver", delay));
    }


    IEnumerator WaitandLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
