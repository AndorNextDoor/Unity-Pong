using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameManager gameManager;
    public int health = 1;

    private void Start()
    {
        StartCoroutine(EnableCollisionAfterDelayCoroutine());
    }
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<AudioSource>().Play();
        gameManager.AddToScore();
        health -= 1;
        if (health <= 0)
        {
            this.gameObject.GetComponent<Animation>().Play();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameManager.RemovePaddles();
            Destroy(this.gameObject,0.5f);
        }
    }

    IEnumerator EnableCollisionAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        // Enable collision after the specified delay
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
