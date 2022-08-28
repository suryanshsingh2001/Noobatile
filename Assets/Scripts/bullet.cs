using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=20f;
   
    Rigidbody2D rigidbody;
    PlayerMovement player;
    float xSpeed;


    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    
    void Update()
    {
        rigidbody.velocity = new Vector2(xSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies")
        {
            
            Destroy(collision.gameObject);
            
        }
        Destroy(gameObject);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        
    }
}
