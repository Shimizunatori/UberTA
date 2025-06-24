using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float time;
    private bool isRunning = true;

    public float GetTime() => time;

    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            timerText.text = time.ToString("F2");
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        time = 0f;
        isRunning = true;
    }
}
