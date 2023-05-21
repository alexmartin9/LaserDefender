using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveDirection;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float paddingHorizontal;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBot;

    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter laser;

    private void Awake()
    {
        laser = FindObjectOfType<Shooter>();
    }
    void Start()
    {
        InitBounds();
    }

    void InitBounds()
    {
        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value) 
    {
        moveDirection = value.Get<Vector2>();
    }

    void Move()
    {
        //Vector2 runVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();  

        newPosition.x = Mathf.Clamp(transform.position.x + moveAmount.x, 
            minBounds.x + paddingHorizontal, maxBounds.x - paddingHorizontal);
        newPosition.y = Mathf.Clamp(transform.position.y + moveAmount.y, 
            minBounds.y + paddingBot, maxBounds.y - paddingTop);
        transform.position = newPosition;
    }

    void OnFire(InputValue value)
    {
        if (laser != null)
        {
            laser.isShooting = value.isPressed;
        }
    }
}
