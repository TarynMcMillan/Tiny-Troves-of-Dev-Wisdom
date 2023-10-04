using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isColliding;
    public GameObject collisionObj;

    void Update() {}

    private void OnTriggerStay2D(Collider2D collision)
    {
        collisionObj = collision.gameObject;
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
