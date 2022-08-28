using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    [Header("Death")]
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    

    [Header("Weapon")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    [Header("Audio")]
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0f, 1f)] float deathFXVolume = 0.75f;

    [SerializeField] AudioClip jumpSound;
    [SerializeField][Range(0f, 1f)] float jumpFXVolume = 0.75f;

    [SerializeField] AudioClip fireSound;
    [SerializeField][Range(0f, 1f)] float fireFXVolume = 0.75f;
    
    
   
    Vector2 moveInput;
    Animator myanimator;
    Rigidbody2D myrigidbody;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myfeetcollider;
    AudioSource audioSource;
    
    float gravityScaleAtStart;
    bool isAlive=true;

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
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

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
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
            audioSource.PlayOneShot(jumpSound,jumpFXVolume);
            myrigidbody.velocity += new Vector2(0f, jumpSpeed);
            
        }

    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        audioSource.PlayOneShot(fireSound,fireFXVolume);
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
            audioSource.PlayOneShot(deathSound,deathFXVolume);
            myrigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
