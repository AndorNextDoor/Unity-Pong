using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameManager gameManager;
    public int health = 1;
    public int maxHealth = 1;

    private void Start()
    {
        StartCoroutine(EnableCollisionAfterDelayCoroutine());
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.PaddleOnSpawn(transform);
    }

    IEnumerator EnableCollisionAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        // Enable collision after the specified delay
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<AudioSource>().Play();
        health -= 1;
        if (health <= 0)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<Animation>().Play();
            gameManager.PaddleDestroyed(maxHealth * 100);
            Destroy(this.gameObject,0.5f);
        }
    }

}
