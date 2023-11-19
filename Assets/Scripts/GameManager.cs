using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Animator camAnimator;
    public int score = 0;
    public int lives = 3;

    [SerializeField] private int currentBalls =1;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballCurrentPos;
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform enemyTree;

    [SerializeField] private GameObject paddles;
    [SerializeField] private int currentPaddleCount;
    private int StrongEnemyCount;
    private int paddleCount;
    
    public float cooldownTime = 3f;
    private bool isCooldown = false;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("ui_manager").transform.GetComponent<UIManager>();
        pointsText = GameObject.FindGameObjectWithTag("ScoreText").transform.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.FindGameObjectWithTag("LivesText").transform.GetComponent<TextMeshProUGUI>();
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        ResetScore();
    }
    void Start()
    {
        StartGame();
    }
public void StartGame()
    {
        enemyTree = Instantiate(paddles).transform;
        StrongEnemyCount = Random.Range(4, 7);
        currentPaddleCount = 0;
        TraverseHierarchy(enemyTree);
    }

    void TraverseHierarchy(Transform currentTransform)
    {
        // Process the current transform or store information as needed
        if (currentTransform.CompareTag("Paddles"))
        {
            // Increment the paddle counter
            paddleCount++;
            currentPaddleCount++;


            // Check if this is the 7th paddle
            if (paddleCount == StrongEnemyCount)
            {
                // Perform the action for the 7th paddle
                ChangePaddle(currentTransform);

                // Reset the counter for the next set of paddles
                paddleCount = 0;
            }
        }

        // Traverse through all children of the current transform
        foreach (Transform child in currentTransform)
        {
            // Recursively call the function for each child
            TraverseHierarchy(child);
        }
    }

    void ChangePaddle(Transform paddleTransform)
    {
        paddleTransform.GetComponent<Paddle>().health = 3;
        paddleTransform.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void AddToScore()
    {
        score += 100;
        pointsText.text = score.ToString();
        if(score%5000 == 0)
        {
            CamShake();
        }
    }
    public void RemoveLives()
    {
        lives -= 1;
        currentBalls--;
        if (lives < 0)
        {
            if (currentBalls == 0)
            {
                ShowRestartMenu();
            }   
        }
        else
        {
            livesText.text = lives.ToString();
            Vector3 ballPosition = playerTranform.position + new Vector3(0,1.5f,0);
            Instantiate(ballPrefab, ballPosition,Quaternion.identity);
            currentBalls++;
        }
    }
    void ResetScore ()
    {
        pointsText.text = score.ToString();
        lives = 3;
        livesText.text = lives.ToString();
    }
    void ShowRestartMenu()
    {
        uiManager.UI_ShowRestartMenu();
    }
    public void SpawnBall()
    {
        currentBalls++;
        ballCurrentPos = GameObject.FindGameObjectWithTag("Ball").transform;
        Instantiate(ballPrefab, ballCurrentPos.position, Quaternion.identity);
    }
    public void RemovePaddles()
    {
        currentPaddleCount--;
        if(currentPaddleCount == 0)
        {
            SpawnNewPaddles();
        }
    }
    void SpawnNewPaddles()
    {
        if (!isCooldown)
        {
            CamShake();
            Destroy(enemyTree.gameObject);
            isCooldown = true;
            lives = 3;
            livesText.text = lives.ToString();
            StartGame();
        }
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
                cooldownTime = 3f; // Reset cooldown time
            }
        }
    }
    IEnumerator PlayerMoveSpeedBuffDuration()
    {
        IncreasePlayerSpeed();
        yield return new WaitForSeconds(6f);
        DecreasePlayerSpeed();
    }
    public void AcceleratePlayer()
    {
        StartCoroutine(PlayerMoveSpeedBuffDuration());
    }
    private void IncreasePlayerSpeed ()
    {
        playerTranform.GetComponent<PlayerMovement>().speed *= 1.5f;
    }
    private void DecreasePlayerSpeed()
    {
        playerTranform.GetComponent<PlayerMovement>().speed /= 1.5f;
    }
    private void CamShake()
    {
        camAnimator.SetTrigger("Shake");
    }
}
