using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
public class GregsFunTime : MonoBehaviour
{
    //private PlayerInput playerInput;
    //private InputAction touchPositionAction;
    //private InputAction touchPressAction;
    //private float movement;
    //private float moveSpeed = 10f;
    //Rigidbody2D rb;

    //private void Awake()
    //{
    //    playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    //    touchPressAction = playerInput.actions["TouchJump"];
    //    touchPositionAction = playerInput.actions["TouchMove"];
    //    rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); 
    //}

    //private void OnEnable()
    //{
    //    touchPressAction.performed += TouchTap;
    //    touchPositionAction.performed -= TouchMove;
    //}

    //private void OnDisable()
    //{
    //    touchPressAction.performed -= TouchTap;
    //    touchPositionAction.performed -= TouchMove;

    //}

    //private void FixedUpdate()
    //{
    //    rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    //}
    //public void TouchTap(InputAction.CallbackContext context)
    //{
    //    rb.gameObject.GetComponent<PlayerTest>().OnJump(context);
    //    rb.gameObject.GetComponent<PlayerTest>().OnInteract(context);
        
    //}
    //public void TouchMove(InputAction.CallbackContext context)
    //{
    //    Vector2 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    //    if(context.ReadValue<Vector2>().x>(0.5*Screen.width))
    //    {
    //        rb.gameObject.GetComponent<PlayerTest>().MoveRight();
    //    }
    //    else
    //    {
    //        rb.gameObject.GetComponent<PlayerTest>().MoveLeft();
    //    }

        
    //}
}
