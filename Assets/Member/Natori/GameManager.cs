using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TimerStop _stopT;
    [SerializeField] private ResultManager _resultM;
    [SerializeField] public float _roundCount;
    [SerializeField] private GameObject[] _stages;
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;

    public int resetC = 0;

    // Start is called before the first frame update
    void Start()
    {
        _stages = new GameObject[]
        {
            _stage1,_stage2,_stage3
        };
        _stages[0].SetActive(true);
    }

    public async void Stage2Instantiate()
    {
        _stages[0].SetActive(false);
        _stages[1].SetActive(true);
        await Task.Delay(1000);
        resetC++;
        _resultM.GetComponent<ResultManager>().touchCount = 0;
        _timer.GetComponent<Timer>().StartTimer();
    }

    public async void Stage3Instantiate()
    {
        resetC++;
        _stages[1].SetActive(false);
        _stages[2].SetActive(true);
        await Task.Delay(1000);
        _resultM.GetComponent<ResultManager>().touchCount = 0;
        _timer.GetComponent<Timer>().StartTimer();
    }
}
