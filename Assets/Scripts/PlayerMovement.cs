using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 9f;
    public float originalSpeed = 9f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // Get the horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement amount
        float movement = horizontalInput * speed;

        // Apply the movement using the Rigidbody
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }

}
