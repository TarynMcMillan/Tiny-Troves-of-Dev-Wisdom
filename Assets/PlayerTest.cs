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
    private bool hasInitiatedMovement = false;

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

        bool isWalkingByKeyboard = moveInput.x != 0;
        bool isWalkingByTouch = touchTap.WasPressedThisFrame();
        print(hasInitiatedMovement);

        // Check if movement is initiated and update the flag only for keyboard input
        if (!hasInitiatedMovement && isWalkingByKeyboard)
        {
            hasInitiatedMovement = true;
        }

        // Check if movement has stopped and update the flag only for keyboard input
        if (hasInitiatedMovement && !isWalkingByKeyboard)
        {
            hasInitiatedMovement = false;
        }

        // Set isWalking based on keyboard input, but only if movement has been initiated
        if (hasInitiatedMovement)
        {
            isWalking = isWalkingByKeyboard;
        }

        // Handle touchscreen input for isWalking
        if (isWalkingByTouch)
        {
            isWalking = true;
        }
        else if (touchTap.WasReleasedThisFrame())
        {
            isWalking = false;
        }

        HandleAnimations();
        HandleSpriteFlip();
    }

    
    //private void SetIsWalking(bool walking)
    //{
    //    // Only set isWalking if it's not already equal to the desired value
    //    if (isWalking != walking)
    //    {
    //        isWalking = walking;
    //    }
    //}

    //if(rb.velocity.x!=0)
    //{
    //    isWalking = true;
    //}
    //else
    //{
    //    isWalking = false;
    //}

    //if (touchTap.WasPressedThisFrame())
    //{
    //    isWalking = true;
    //}
    //if (touchTap.WasReleasedThisFrame())
    //{
    //    isWalking = false;
    //}

    //HandleAnimations();
    //HandleSpriteFlip();


   

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

    //private void StartTouch(InputAction.CallbackContext context)
    //{
        
    //    isWalking = true;
    //}

    //private void EndTouch(InputAction.CallbackContext context)
    //{
        
    //    isWalking = false;
    //}

    private void HandleAnimations()
    {
        animator.SetBool("isMoving", isWalking);
    }
    public void TouchMove(InputAction.CallbackContext context)
    {
            
            moveInput = context.ReadValue<Vector2>();
            
       

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
