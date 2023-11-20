using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Animator camAnimator;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameManager gameManager;
    private bool isOnPause = false;

    void Update()
    {
        if (isOnPause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                Scene currentscene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentscene.name);
                isOnPause = false;
                pauseMenu.SetActive(false);
                gameManager.StartLevel();

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                isOnPause = true;
                pauseMenu.SetActive(true);
            }
        }
    }
    public void UI_ShowPauseMenu()
    {
        Time.timeScale = 0;
        isOnPause = true;
        pauseMenu.SetActive(true);
    }
    public void UpdateScore(int points)
    {
        scoreText.text = points.ToString();
    }
    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }
    public void CamShake()
    {
        camAnimator.SetTrigger("Shake");
    }
}
