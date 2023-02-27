using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCotroller : MonoBehaviour
{
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private float baseSpeed = 20f;
    [SerializeField] private float torqueAmount;

    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rbPlayer;
    
    public bool canMove = true;
    
    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            Rotation();
            Boost();
        }
    }
    
    private void Boost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rbPlayer.AddTorque(-torqueAmount);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rbPlayer.AddTorque(torqueAmount);
        }
    }
}
