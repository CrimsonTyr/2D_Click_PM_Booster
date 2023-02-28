using UnityEngine;
using System;

public class InputIntervals : MonoBehaviour
{
    [SerializeField] ClickPmBoosterGame game;
    [SerializeField] Statistics stats;
    private DateTime lastInputTime;
    private double interval;
    private double lowInterval;
    private double highInterval;

    public double GetInterval() => interval;

    private void Awake()
    {
        ResetIntervals();
    }

    // Update the value of "interval" to be equal to the number of milliseconds
    // between the last call of this function and now
    public void UpdateIntervals()
    {
        interval = (DateTime.Now - lastInputTime).TotalMilliseconds;
        lastInputTime = DateTime.Now;
    }

    public void UpdateTextsIntervals()
    {
        if (interval < lowInterval)
        {
            lowInterval = interval;
            stats.UpdateLowInterval((int)lowInterval);
        }
        if (interval > highInterval)
        {
            highInterval = interval;
            stats.UpdateHighInterval((int)highInterval);
        }
    }

    // Reset intervals and "simulates" the first user click by
    // updating the value of lastInputTime
    public void ResetIntervals()
    {
        lastInputTime = DateTime.Now;
        lowInterval = 90000;
        highInterval = 0;
    }
}