using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")] private float _speed;

    [SerializeField] private DrawLine _drawL;
    public bool _moveFlag = false;
    public bool _clearFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_moveFlag || _clearFlag) return;
        var _dest = _drawL.GetComponent<DrawLine>().lineRenderer.GetPosition(0);
        if (_dest == null) return;
        transform.position = Vector2.MoveTowards(transform.position, _dest, _speed * Time.deltaTime);
    }
}
