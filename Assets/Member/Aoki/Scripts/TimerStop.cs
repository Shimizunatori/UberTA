using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStop : MonoBehaviour
{
    public Timer timer;
    public ResultManager resultManager;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            timer.StopTimer();
            Debug.Log("uaa");
            float clearTime = timer.GetTime();
            resultManager.ShowResult(clearTime);
        }
    }
}
