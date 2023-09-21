using UnityEngine;
using UnityEngine.InputSystem;

public class TouchControls : MonoBehaviour
{
    public PlayerTest playerController;

    private void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Calculate the screen's center for reference
            float screenCenterX = Screen.width * 0.5f;

            if (touch.position.x < screenCenterX)
            {
                // Left side of the screen (movement)
                //playerController.OnTouchMoveLeft(touch.position);
            }
            else
            {
                // Right side of the screen (jumping)
                //playerController.OnTouchJumpRight(touch.position);
            }
        }
    }


}
