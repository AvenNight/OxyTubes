using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TimerObj TimerObj;
    public UI2DSprite TimeMask;
    public UILabel TimeLeft;

    private float startTime;

    public void Set(float time, float delta)
    {
        startTime = time;
        TimerObj.Set(time, delta);
        TimerObj.OnTick = (t) =>
        {
            TimeMask.fillAmount = t / startTime;
            var ts = TimeSpan.FromSeconds(t);
            TimeLeft.text = ts.ToString(@"mm\:ss");
        };
    }
}
