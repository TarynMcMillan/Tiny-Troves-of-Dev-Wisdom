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

    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private float movement;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchJump"];
        touchPositionAction = playerInput.actions["TouchMove"];
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInteracting = false;
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchTap;
        touchPositionAction.performed -= TouchMove;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchTap;
        touchPositionAction.performed -= TouchMove;

    }
    private void Update()
    {
        // Ground check using a small circle overlap

        isInteracting = Physics2D.OverlapCircle(groundCheck.position, 0.2f, chestLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

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
    public void TouchMove(InputAction.CallbackContext context)
    {
        if (!isInteracting)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            if (context.ReadValue<Vector2>().x > (0.5 * Screen.width))
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isInteracting)
        {
            moveInput = context.ReadValue<Vector2>();
        }
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
        moveInput.x = -1f;
    }

    public void MoveRight()
    {
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
