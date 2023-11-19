using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool isCooldown = false;
    private float cooldownTime = 0.5f;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCooldown)
        {
            Destroy(collision.gameObject);
            gameManager.RemoveLives();
            isCooldown = true;
        }
    }
}
