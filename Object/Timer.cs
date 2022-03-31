using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    private bool isStart;       // 타이머 시작 여부

    private ITimerEndListener listener;

    void Update()
    {
        if (isStart)
        {
            time -= Time.deltaTime;
            // 타이머 종료
            if (time <= 0)
            {
                isStart = false;
                time = 0;
                listener.OnTimerEnd();
            }
        }
    }

    public void startTimer(float time, ITimerEndListener listener)
    {
        this.time = time;
        this.listener = listener;
        isStart = true;
    }

    /// <summary>
    /// 타이머 시간을 시분초로 반환
    /// </summary>
    /// <returns>{hour, minute, second}</returns>
    public int[] getHMS()
    {
        int hour = (int)time / 60 / 60;
        int minute = (int)time / 60 % 60;
        int second = (int)time % 60 % 60;

        return new int[] { hour, minute, second };
    }
}