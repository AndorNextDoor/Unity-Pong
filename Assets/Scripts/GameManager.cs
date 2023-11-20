using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public int lives = 3;
    public int score;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballCurrentPos;
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform enemyTree;

    [SerializeField] private GameObject paddles;
    [SerializeField] private int currentPaddleCount;
    private int StrongEnemyCount;
    
    void Start()
    {
        StartLevel();
    }
    public void StartLevel()
    {
        enemyTree = Instantiate(paddles).transform;
        StrongEnemyCount = Random.Range(4, 7);
        currentPaddleCount = 0;
    }

    public void PaddleOnSpawn(Transform currentPaddleTransform)
    {
        currentPaddleCount++;
        if(currentPaddleCount % StrongEnemyCount == 0 )
        {
            UpgradePaddle(currentPaddleTransform);
        }
    }
    public void PaddleDestroyed(int toScore)
    {
        score += toScore;
        currentPaddleCount--;
        uiManager.UpdateScore(score);
        if (score % 2500 == 0)
        {
            uiManager.CamShake();
        }
        if (currentPaddleCount == 0)
        {
            uiManager.CamShake();
            Destroy(enemyTree.gameObject);
            StartLevel();
        }
    }
    void UpgradePaddle(Transform paddleTransform)
    {
        paddleTransform.GetComponent<Paddle>().health = 3;
        paddleTransform.GetComponent<Paddle>().maxHealth = 3;
        paddleTransform.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    public void BallDestroyed(bool containsLives)
    {
        if (containsLives)
        {
            lives--;
        }
          
        switch (lives)
        {
        case > 0:
            uiManager.UpdateLives(lives);
                if (GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
                {
                    SpawnBall(true);
                }
        break;
        case 0:
            if (GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
            {
            uiManager.UpdateLives(lives);
            uiManager.UI_ShowPauseMenu();
            }
        break;
        }
    }
    public void SpawnBall(bool isContainingLives)
    {
        GameObject ball = Instantiate(ballPrefab, playerTranform.position, Quaternion.identity);
        ball.GetComponent<BallMovement>().isContainingLive = isContainingLives;
    }
   
    public void AcceleratePlayer()
    {
        StartCoroutine(PlayerMoveSpeedBuffDuration());
    }
    IEnumerator PlayerMoveSpeedBuffDuration()
    {
        IncreasePlayerSpeed();
        yield return new WaitForSeconds(6f);
        DecreasePlayerSpeed();
    }
    private void IncreasePlayerSpeed ()
    {
        playerTranform.GetComponent<PlayerMovement>().speed *= 1.5f;
    }
    private void DecreasePlayerSpeed()
    {
        playerTranform.GetComponent<PlayerMovement>().speed /= 1.5f;
    }
}
