using UnityEngine;

public class Timer : MonoBehaviour
{
    public TimerObj TimerObj;
    public UI2DSprite TimeMask;

    private float startTime;

    public void Set(float time, float delta)
    {
        startTime = time;
        TimerObj.Set(time, delta);
        TimerObj.OnTick = (t) =>
        {
            TimeMask.fillAmount = TimerObj.TimeLeft / startTime;
        };
    }
}
