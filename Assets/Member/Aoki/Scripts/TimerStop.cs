using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStop : MonoBehaviour
{
    public Timer timer;
    public ResultManager resultManager;
    public GameManager gameManager;
    [SerializeField]
    private float clearNum;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            resultManager.GetComponent<ResultManager>().touchCount++;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (resultManager.touchCount >= clearNum && gameManager.resetC >= 2)
            {
                triggered = true;
                timer.StopTimer();
                Debug.Log("uaa");
                float clearTime = timer.GetTime();
                resultManager.ShowResult(clearTime);
            }
            switch (gameManager.GetComponent<GameManager>().resetC)
            {
                case 0:
                    if (resultManager.GetComponent<ResultManager>().touchCount >= gameManager.GetComponent<GameManager>()._roundCount)
                    {
                        timer.GetComponent<Timer>().StopTimer();
                        gameManager.GetComponent<GameManager>().Stage2Instantiate();
                    }
                    break;
                case 1:
                    if (resultManager.GetComponent<ResultManager>().touchCount >= gameManager.GetComponent<GameManager>()._roundCount)
                    {
                        timer.GetComponent<Timer>().StopTimer();
                        gameManager.GetComponent<GameManager>().Stage3Instantiate();
                    }
                    break;
            }
        }
    }
}
