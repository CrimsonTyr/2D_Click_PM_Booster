using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text score;
    [SerializeField] TMPro.TMP_Text highestCombo;
    [SerializeField] TMPro.TMP_Text lowInterval;
    [SerializeField] TMPro.TMP_Text highInterval;
    [SerializeField] TMPro.TMP_Text totalClicks;
    [SerializeField] TMPro.TMP_Text missedClicks;
    [SerializeField] TMPro.TMP_Text fastClicks;
    [SerializeField] TMPro.TMP_Text goodClicks;
    [SerializeField] TMPro.TMP_Text slowClicks;
    [SerializeField] TMPro.TMP_Text accuracy;

    public void ResetAllStatistics()
    {
        score.text = "0";
        highestCombo.text = "0";
        lowInterval.text = "none";
        highInterval.text = "none";
        totalClicks.text = "0";
        missedClicks.text = "0";
        accuracy.text = "100.00%";
    }

    public void UpdateScore(int value)
    {
        score.text = value.ToString();
    }

    public void UpdateHighestCombo(int value)
    {
        highestCombo.text = "x" + value.ToString();
    }

    public void UpdateLowInterval(int value)
    {
        lowInterval.text = value.ToString() + "ms";
    }

    public void UpdateHighInterval(int value)
    {
        highInterval.text = value.ToString() + "ms";
    }

    public void UpdateTotalClicks(int value)
    {
        totalClicks.text = value.ToString();
    }

    public void UpdateMissedClicks(int value)
    {
        missedClicks.text = value.ToString();
    }

    public void UpdateFastClicks(int value)
    {
        fastClicks.text = value.ToString();
    }

    public void UpdateGoodClicks(int value)
    {
        goodClicks.text = value.ToString();
    }

    public void UpdateSlowClicks(int value)
    {
        slowClicks.text = value.ToString();
    }

    public void UpdateAccuracy(float value)
    {
        accuracy.text = value.ToString("F2") + "%";
    }
}