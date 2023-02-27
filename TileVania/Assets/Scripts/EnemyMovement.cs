using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D enemyRigidbody;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void Flipping()
    {
        transform.localScale = new Vector2(Mathf.Sign(-enemyRigidbody.velocity.x), 1f);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            moveSpeed = -moveSpeed;
            Flipping();
        }
    }
}
