using System.Collections;
using UnityEngine;

public class ClickPmBoosterGame : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private InputIntervals inputIntervals;
    [SerializeField] private GameObject clickInfos;
    [SerializeField] private TMPro.TMP_Text comboText;
    [SerializeField] private TMPro.TMP_Text speedText;
    [SerializeField] private TMPro.TMP_Text buttonText;
    [SerializeField] private Statistics stats;
    [SerializeField] private AudioSounds sounds;
    private SpriteRenderer squareToClick;
    private bool isPlaying;
    private bool isInPause;
    private bool isRightSquare;
    private int score;
    private int combo;
    private int highestCombo;
    private int totalClicks;
    private int missedClicks;
    private int fastClicks;
    private int goodClicks;
    private int slowClicks;
    public static ClickPmBoosterGame instance;

    public bool IsPlaying() => isPlaying;

    public bool IsInPause() => isInPause;

    // Singleton pattern ensuring that there is only one instance of ClickPmBoosterGame
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        isPlaying = false;
        isInPause = false;
        instance = this;
    }

    // If the game has not started, does nothing
    private void Update()
    {
        if (!isPlaying || isInPause)
            return;
        if (Input.GetMouseButtonDown(0))
            HandleUserInput();
    }

    public void StartGame()
    {
        isRightSquare = false;
        score = 0;
        combo = 0;
        highestCombo = 0;
        totalClicks = 0;
        missedClicks = 0;
        fastClicks = 0;
        goodClicks = 0;
        slowClicks = 0;
        stats.ResetAllStatistics();
        inputIntervals.ResetIntervals();
        NewRandomSquareToClick();
        isPlaying = true;
    }

    public void EndGame()
    {
        sounds.PlayGameFinishSound();
        isPlaying = false;
        if (squareToClick != null)
        {
            squareToClick.color = Color.white;
            squareToClick = null;
        }
    }

    public void PauseMode(bool state)
    {
        isInPause = state;
        if (isInPause)
            buttonText.text = "Pause...";
        else
            buttonText.text = "Playing !";
    }

    public void IsRightSquare(bool value)
    {
        isRightSquare = value;
    }

    // Update all statistics depending on the square that has been clicked by the user
    // Then, get a new square to click on
    private void HandleUserInput()
    {
        inputIntervals.UpdateIntervals();
        totalClicks += 1;
        if (isRightSquare)
            UserClickedRightSquare();
        else
            UserClickedWrongSquare();
        comboText.text = "x" + combo.ToString();
        clickInfos.gameObject.SetActive(true);
        UpdateClicksStatistics();
        StartCoroutine(NewSquareToClick());
    }

    private void UserClickedRightSquare()
    {
        sounds.PlayRightSquareSound();
        score += (10 * (combo + 1) * EvaluateUserSpeed());
        combo += 1;
        stats.UpdateScore(score);
        if (combo > highestCombo)
        {
            highestCombo = combo;
            stats.UpdateHighestCombo(highestCombo);
        }
        SetComboTextColor();
        inputIntervals.UpdateTextsIntervals();
    }

    private int EvaluateUserSpeed()
    {
        double interval = inputIntervals.GetInterval();
        int evaluation;

        if (interval <= 500)
        {
            speedText.text = "Fast !";
            speedText.color = Color.blue;
            evaluation = 4;
            fastClicks += 1;
            stats.UpdateFastClicks(fastClicks);
        }
        else if (interval <= 750)
        {
            speedText.text = "Good";
            speedText.color = Color.green;
            evaluation = 2;
            goodClicks += 1;
            stats.UpdateGoodClicks(goodClicks);
        }
        else
        {
            speedText.text = "Slow...";
            speedText.color = Color.red;
            evaluation = 1;
            slowClicks += 1;
            stats.UpdateSlowClicks(slowClicks);
        }
        speedText.gameObject.SetActive(true);
        return evaluation;
    }

    private void SetComboTextColor()
    {
        if (combo < 5)
            comboText.color = Color.red;
        else if (combo < 30)
            comboText.color = Color.yellow;
        else if (combo < 80)
            comboText.color = Color.green;
        else
            comboText.color = Color.blue;
    }

    private void UserClickedWrongSquare()
    {
        sounds.PlayWrongSquareSound();
        combo = 0;
        missedClicks += 1;
        comboText.color = Color.red;
        speedText.gameObject.SetActive(false);
    }

    // The color of the previous square that the user had to click on changes
    // for 0.05s depending on the input before a new square to click on is set
    private IEnumerator NewSquareToClick()
    {
        if (squareToClick != null)
            if (isRightSquare)
                squareToClick.color = Color.green;
            else
                squareToClick.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        if (squareToClick != null)
            squareToClick.color = Color.white;
        if (isPlaying)
            NewRandomSquareToClick();
        isRightSquare = false;
        inputIntervals.UpdateIntervals();
    }

    private void NewRandomSquareToClick()
    {
        squareToClick = board.GetRandomSquare();
        squareToClick.color = Color.cyan;
    }

    private void UpdateClicksStatistics()
    {
        float accuracy;

        stats.UpdateTotalClicks(totalClicks);
        stats.UpdateMissedClicks(missedClicks);
        accuracy = ((float)totalClicks - (float)missedClicks) * 100 / (float)totalClicks;
        stats.UpdateAccuracy(accuracy);
    }
}