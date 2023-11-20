using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAdditionalBall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            gameManager.SpawnBall(false);
            Destroy(this.gameObject);
        }
    }
}
