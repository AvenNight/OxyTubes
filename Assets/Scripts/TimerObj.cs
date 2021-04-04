using System;
using UnityEngine;

public class TimerObj : MonoBehaviour
{
    public Action<float> OnTick;
    public bool StopOnZero = false;

    public float TimeLeft;// { get; protected set; }
    private bool isLastZero = false;

    public void Set(float time, float delta)
    {
        enabled = true;

        if (time < delta || Mathf.Abs(TimeLeft - time) > delta)
        {
            TimeLeft = time;
            isLastZero = false;
        }
    }

    public void Ping() => OnTick?.Invoke(TimeLeft);

    void Update()
    {
        if (OnTick == null) return;

        if (isLastZero == true && StopOnZero == true)
        {
            enabled = false;
            return;
        }

        TimeLeft -= Time.deltaTime;

        //Если время вышло
        if (TimeLeft <= 0)
        {
            TimeLeft = 0;
            isLastZero = true;
        }

        OnTick(TimeLeft);
    }
}