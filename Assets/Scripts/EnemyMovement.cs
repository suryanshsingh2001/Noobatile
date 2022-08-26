using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myrigidbody;
    
        
    
    void Start()
    {
        myrigidbody=GetComponent<Rigidbody2D>();    
    
    
    }

    void Update()
    {
        myrigidbody.velocity = new Vector2(moveSpeed, 0f);
    }


   void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        flipEnemyFacing();
    }

   void flipEnemyFacing()
    {
        transform.localScale = new Vector2(-MathF.Sign(myrigidbody.velocity.x), 1f);
    }
}
