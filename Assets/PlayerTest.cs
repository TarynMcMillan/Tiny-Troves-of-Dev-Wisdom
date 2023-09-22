using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isInteracting;
    private bool isWalking;
    
    private Vector2 moveInput;
    Vector2 distance;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask chestLayer;

    private PlayerInput playerInput;
    private InputAction touchMove;
    private InputAction touchTap;
    private InputAction touchJump;
    private float movement;
    Animator animator;

    GameObject player;
   
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchJump = playerInput.actions["TouchJump"];
        touchMove = playerInput.actions["TouchMove"];
        touchTap = playerInput.actions["TouchTap"];
        player = playerInput.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInteracting = false;
        animator = GetComponent<Animator>();
        
        //controls.Player.TouchTap.canceled += ctx => EndTouch(ctx);
        
        // controls.Player.TouchJump.started+=ctx => StartTouch(ctx);
    }
    
    private void Update()
    {

        isInteracting = Physics2D.OverlapCircle(groundCheck.position, 0.2f, chestLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
        if(rb.velocity.x!=0)
        {
            isWalking = true;
        }

        //if(touchTap.WasPressedThisFrame())
        //{
        //    isWalking = true;
        //}
        //if(touchTap.WasReleasedThisFrame())
        //{
        //    isWalking = false;
        //}

        HandleAnimations();
        HandleSpriteFlip();
    }

   

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
    public void TouchTap(InputAction.CallbackContext context)
    {
       
        if (isInteracting)
        {
            OnJump(context);
            OnInteract(context);
        }

    }


    private void HandleSpriteFlip()
    {
        if(moveInput.x<0)
        {
            player.GetComponent<SpriteRenderer>().flipX= false;
        }
        else if (moveInput.x>0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        
        isWalking = true;
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        
        isWalking = false;
    }

    private void HandleAnimations()
    {
        animator.SetBool("isMoving", isWalking);
    }
    public void TouchMove(InputAction.CallbackContext context)
    {
            
            moveInput = context.ReadValue<Vector2>();
            // Vector2 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            //if (context.ReadValue<Vector2>().y > (0.5 * Screen.width))
            //{
            //    MoveRight();
            //}
            //else
            //{
            //    MoveLeft();
            //}
       

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        
            moveInput = context.ReadValue<Vector2>();
       
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded && !isInteracting)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void MoveLeft()
    {
        print("moving left");
        moveInput.x = -1f;
    }

    public void MoveRight()
    {
        print("moving right");
        moveInput.x = 1f;
    }
   
    public void OnInteract(InputAction.CallbackContext context)
    {
        
        if(isInteracting)
        {
            print("opening a chest");
        }
        //if (context.started)
        //{
        //    isInteracting = true;
        //}
        //else if (context.canceled)
        //{
        //    isInteracting = false;
        //}
    }


}
