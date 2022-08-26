using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
   
    
    
    
    Vector2 moveInput;
    Animator myanimator;
    Rigidbody2D myrigidbody;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myfeetcollider;
    
    float gravityScaleAtStart;
    bool isAlive=true;

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        
        mybodyCollider = GetComponent<CapsuleCollider2D>();
        myfeetcollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myrigidbody.gravityScale;

    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Death();

    }

    
    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myrigidbody.velocity.y);      
        myrigidbody.velocity = playerVelocity;
      

        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myanimator.SetBool("isRunning", playerHasHorizontalSpeed);



    }
    void OnJump(InputValue value)
    {
        
        if (!isAlive)
        {
            return;
        }
        if (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myrigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }


    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void ClimbLadder()
    {
        if (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {           
            myrigidbody.gravityScale = gravityScaleAtStart;
            myanimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbSpeed = new Vector2(myrigidbody.velocity.x, moveInput.y * jumpSpeed);
        myrigidbody.velocity = climbSpeed;
        myrigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;

        myanimator.SetBool("isClimbing", playerHasVerticalSpeed);
               
    }
    void Death()
    {
        if (mybodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            
            isAlive = false;
            myanimator.SetTrigger("Dying");
            myrigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();

           
        }
    }
}
