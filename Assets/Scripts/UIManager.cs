using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{   private static UIManager instance;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private bool isOnStart = true;
    [SerializeField] private bool isOnPause = false;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this object
            instance = this;
            Time.timeScale = 0;

            // Mark this object to not be destroyed when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (isOnStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                isOnStart = false;
                startMenu.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        if(isOnPause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Time.timeScale = 1;
                Scene currentscene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentscene.name);
                isOnPause = false;
                pauseMenu.SetActive(false);
                gameManager.StartGame();


            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        if(!isOnStart && !isOnPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                isOnPause = true;
                pauseMenu.SetActive(true);
            }
        }
    }
    public void UI_ShowRestartMenu()
    {
        isOnPause = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
