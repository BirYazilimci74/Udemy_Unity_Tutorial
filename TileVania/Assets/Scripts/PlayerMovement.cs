using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private Vector2 deathKick;
    

    private AnimationController animationController;
    private CapsuleCollider2D playerCapsuleCollider;
    private BoxCollider2D playerFeetCollider;
    private Rigidbody2D playerRigidbody;
    private bool isAlive = true;
    private float gravityScale;
    private Vector2 moveInput;
    
    private void Start()
    {
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        animationController = GetComponent<AnimationController>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScale = playerRigidbody.gravityScale;
    }
    
    private void Update()
    {
        if (!isAlive) { return; }
        ClimbLadder();
        FlipSprite(); 
        Run();
        Die();
    }
    
    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (!playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animationController.ClimbingAnimation(false);
            playerRigidbody.gravityScale = gravityScale;
            return;
        }
        
        bool hasPlayerMovement = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        animationController.ClimbingAnimation(hasPlayerMovement);

        playerRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = climbVelocity;
    }
    
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        
        bool hasPlayerMovement = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        animationController.RunAnimation(hasPlayerMovement);
    }

    private void FlipSprite()
    {
        bool hasPlayerMovement = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (hasPlayerMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animationController.DeathAnimation();
            playerRigidbody.velocity = deathKick;
        }
    }
}
