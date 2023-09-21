using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isInteracting;
    private Vector2 moveInput;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask chestLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInteracting = false;
    }

    private void Update()
    {
        // Ground check using a small circle overlap
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isInteracting = Physics2D.OverlapCircle(groundCheck.position, 0.1f, chestLayer);

        // Handle keyboard input for movement
        //float horizontalInput = Input.GetAxis("Horizontal");


        //// Check for chest interaction
        //if (isInteracting)
        //{
        //    Debug.Log("Chest is opening");
        //}
    }

   
    private void FixedUpdate()
    {
        // Handle movement
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the input vector from the Input System
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.started}{isGrounded}{isInteracting}");
        if (context.started && isGrounded && !isInteracting)
        {
            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnTouchMoveLeft(Vector2 touchPosition)
    {
        // Handle touch movement on the left side
        // You can implement custom logic here based on the touch position
    }

    public void OnTouchJumpRight(Vector2 touchPosition)
    {
        // Handle touch jump on the right side
        // You can implement custom logic here based on the touch position
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
