using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerStartButton : MonoBehaviour
{
    [SerializeField] private Animator countdownAnimator;
    [SerializeField] private Timer timer;
    [SerializeField] private AudioSounds sounds;
    private Button startButton;
    private Image startButtonImage;
    private TMPro.TMP_Text buttonText;

    private void Awake()
    {
        startButton = GetComponent<Button>();
        startButtonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMPro.TMP_Text>();
        countdownAnimator.gameObject.SetActive(false);
    }

    public void ResetButtonState()
    {
        startButtonImage.color = startButton.colors.normalColor;
        buttonText.text = "Play";
        startButton.interactable = true;
    }

    // This function does something only if the user has not started a game yet
    // It starts the countdown animation before the game starts and then
    // starts the timer that determines the remaining time before the game ends
    public void TimerStartButtonFunction()
    {
        if (countdownAnimator.gameObject.activeSelf || timer.IsRunning())
            return;
        countdownAnimator.gameObject.SetActive(true);
        countdownAnimator.Play("CountdownAnimation");
        buttonText.text = "Get Ready...";
        startButton.interactable = false;
        startButtonImage.color = startButton.colors.pressedColor;
        StartCoroutine(WaitToStartTimer());
    }

    // Wait for the countdown animation to end before starting the timer
    private IEnumerator WaitToStartTimer()
    {
        for (int i = 0; i < 4; i += 1)
        {
            sounds.PlayCountdownBeat();
            yield return new WaitForSeconds(1f);
        }
        countdownAnimator.gameObject.SetActive(false);
        timer.StartTimer(90);
        ClickPmBoosterGame.instance.StartGame();
        buttonText.text = "Playing !";
    }
}