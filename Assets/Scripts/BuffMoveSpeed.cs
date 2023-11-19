using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMoveSpeed : MonoBehaviour
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
            gameManager.AcceleratePlayer();
            Destroy(this.gameObject);
        }
    }
}
