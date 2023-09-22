using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    private NewPlayerMovement playerMovement;
    private Vector2 screenPos;
    PlayerInput playerInput;
    InputAction mousePosition;
    InputAction touchPosition;
    InputAction touchPress;
    InputAction mouseClick;

    private void Start()
    {
        playerMovement = GetComponent<NewPlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        
        touchPosition = playerInput.actions["TouchPosition"];
        mousePosition = playerInput.actions["MousePosition"];

        touchPress = playerInput.actions["TouchPress"];
        mouseClick = playerInput.actions["MouseClick"];
        //Debug.Log($"{touchPress} {mouseClick} {touchPosition}{mousePosition}");
    }

    //private void OnEnable()
    //{
    //    touchPress.performed += OnTouchPress;
    //}

    //private void OnDisable()
    //{
    //    touchPress.performed -= OnTouchPress;
    //}

    public void OnTouchPress(InputAction.CallbackContext context)
    {
       
        if (Touchscreen.current.touches.Count > 0)
        {
            screenPos = touchPosition.ReadValue<Vector2>();
            SelectChest(screenPos);
        }
    }

    private void SelectChest(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // Debug information about the ray
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

        // Perform raycast
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Debug.Log("Hit point: " + hit.point);

            // Check the tag or layer of the hit collider
            if (hit.collider.CompareTag("Chest"))
            {
                Transform chestTransform = hit.collider.transform;
                playerMovement.MoveTowardsChest(chestTransform);
            }
            else
            {
                Debug.Log("Hit collider does not have the 'Chest' tag.");
            }
        }
        else
        {
            Debug.Log("No hit collider.");
        }
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {

        screenPos = mousePosition.ReadValue<Vector2>();
        SelectChest(screenPos);
    }
}


      
    










