using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinAudio;
    [SerializeField] int pointForCoinPickup=100;

    bool coinWasCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !coinWasCollected)
        {
            coinWasCollected = true;
            FindObjectOfType<GameSession>().addToScore(pointForCoinPickup);
            AudioSource.PlayClipAtPoint(coinAudio, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }
}
