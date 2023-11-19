using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    public float cooldownTime = 0.5f; 
    private bool isCooldown = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1f, 1f).normalized * 2;
        isCooldown = true;
    }

    private void Update()
    {
        if (isCooldown)
        {
            // Reduce cooldown time
            cooldownTime -= Time.deltaTime;

            // Check if cooldown is over
            if (cooldownTime <= 0f)
            {
                isCooldown = false;
                cooldownTime = 0.5f; // Reset cooldown time
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCooldown)
        {
            
            Vector2 reflect = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflect.normalized * speed;

            // Set cooldown to prevent rapid collisions
            isCooldown = true;
        }
    }
}
