using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public Vector2 moveInput;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Handle movement using the Input System
        Vector2 movement = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the input vector from the Input System
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        print("1");
        // Handle jumping using the Input System
        if (context.started)
        {
            print("2");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

