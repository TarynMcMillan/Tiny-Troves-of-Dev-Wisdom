using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed = 10f;
    PlayerCollision _collision;

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        _collision= GetComponent<PlayerCollision>();
    }

    //private void OnMove(InputValue value)
    //{
    //    _rigidbody.velocity = value.Get<Vector2>() * _speed;
    //}

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _rigidbody.velocity = ctx.ReadValue<Vector2>() * _speed;
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        
        if (_collision.isColliding)
        {
            _collision.collisionObj.GetComponent<Animator>().SetTrigger("open");
            FindObjectOfType<ChestSpawner>().DisplayAdvice(_collision.collisionObj);
        }
    }

}
