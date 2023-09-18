using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    bool isColliding;
    GameObject collisionObj;

    void Update()
    {
        if(isColliding)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                collisionObj.GetComponent<Animator>().SetTrigger("open");
                FindObjectOfType<ChestSpawner>().DisplayAdvice(collisionObj);
                isColliding = false;
            }
        }
    }

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
