using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TimerStartButton timerStartButton;
    [SerializeField] TMPro.TMP_Text min;
    [SerializeField] TMPro.TMP_Text sec;
    [SerializeField] TMPro.TMP_Text millisec;
    [SerializeField] AudioSounds sounds;
    private float timeRemaining = 0;
    private bool isRunning = false;
    private bool hasPlayedLowTimeSound;

    public bool IsRunning() => isRunning;

    // While the timer is running, the time remaining decreases each frame
    // by the value of the interval between the last frame and the current one
    private void Update()
    {
        if (!isRunning || ClickPmBoosterGame.instance.IsInPause())
            return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = timeRemaining > 0 ? timeRemaining : 0;
            DisplayTimer();
        }
        else
        {
            isRunning = false;
            ClickPmBoosterGame.instance.EndGame();
            timerStartButton.ResetButtonState();
        }
        if (!hasPlayedLowTimeSound && timeRemaining <= 10)
        {
            sounds.PlayLowTimeSound();
            hasPlayedLowTimeSound = true;
        }
    }

    public void StartTimer(float timeInSeconds)
    {
        if (isRunning)
            return;
        timeRemaining = timeInSeconds;
        isRunning = true;
        hasPlayedLowTimeSound = false;
    }

    // Display the time remaining in the UI elements of the game in
    // minutes : seconds : milliseconds
    private void DisplayTimer()
    {
        int minues = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        int milliseconds = Mathf.FloorToInt(timeRemaining % 1 * 100);

        min.text = string.Format("{0:00}", minues);
        sec.text = string.Format("{0:00}", seconds);
        millisec.text = string.Format("{0:00}", milliseconds);
    }
}