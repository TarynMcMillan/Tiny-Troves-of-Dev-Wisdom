using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    private NewPlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<NewPlayerMovement>();
    }

    public void OnChestSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Get the mouse/touch position in screen space
            Vector2 screenPos =context.ReadValue<Vector2>();


            // Cast a ray from the camera through the screen position
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
    }





}



