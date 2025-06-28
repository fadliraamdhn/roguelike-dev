using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;

    private bool isPaused = false;

    void Start()
    {
        // Ensure it's off at start
        pausePanel.SetActive(false);

        // Hook up button listeners
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // freeze time
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}
