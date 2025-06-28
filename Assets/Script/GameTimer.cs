using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int startTimeInSeconds = 60; // Change this for different countdown duration

    private float timeRemaining;
    private bool isRunning = true;

    void Start()
    {
        timeRemaining = startTimeInSeconds;
    }

    void Update()
    {
        if (!isRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
            isRunning = false;
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        Debug.Log("Time's up!");
        // Optional: trigger lose condition, restart, etc.
    }
}
