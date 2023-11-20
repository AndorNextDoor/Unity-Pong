using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(collision.gameObject);
        StartCoroutine(Delay(collision.gameObject.GetComponent<BallMovement>().isContainingLive));
        

    }
    IEnumerator Delay(bool isContainingLive)
    {
        yield return new WaitForSeconds(0.15f);
        gameManager.BallDestroyed(isContainingLive);
    }
}
