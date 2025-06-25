using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®‘¬“x")] private float _speed;

    [SerializeField] private DrawLine _drawL;
    public bool _moveFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_moveFlag) return;
        var _dest = _drawL.GetComponent<DrawLine>().lineRenderer.GetPosition(0);
        transform.position = Vector2.MoveTowards(transform.position, _dest, _speed * Time.deltaTime);
    }
}
