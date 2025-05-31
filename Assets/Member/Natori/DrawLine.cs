using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField,Header("線のマテリアル")] private Material lineMaterial;
    [SerializeField,Header("線の色")] private Color lineColor;
    [Range(0.1f, 0.5f)]
    [SerializeField,Header("線の幅")] private float lineWidth;

    private int inputCount = 0;
    [SerializeField]
    private GameObject startPosObj;
    [SerializeField]
    private GameObject endPosObj;
    private Vector3 mousePos1;
    private Vector3 mousePos2;

    GameObject lineObj;
    LineRenderer lineRenderer;
    List<Vector2> linePoints;

    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector2>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        positions = new Vector3[]
        {
            startPosObj.transform.position,
            endPosObj.transform.position
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inputCount == 0)
        {
            Debug.Log("start位置");
            Vector3 touchMousePos1 = Input.mousePosition;
            touchMousePos1.z = -1.0f;
            mousePos1 = Camera.main.ScreenToWorldPoint(touchMousePos1);
            startPosObj.transform.position = mousePos1;
            positions[0] = startPosObj.transform.position;
            inputCount++;
            //_addLineObject();
        }
        if (Input.GetMouseButtonDown(0) && inputCount == 1)
        {
            Debug.Log("end位置");
            Vector3 touchMousePos2 = Input.mousePosition;
            touchMousePos2.z = -1.0f;
            mousePos2 = Camera.main.ScreenToWorldPoint(touchMousePos2);
            endPosObj.transform.position = mousePos2;
            positions[1] = endPosObj.transform.position;
            inputCount++;
            _addLineObject();
            _addPositionDataToLineRenderer();
        }
        if (Input.GetMouseButton(0))
        {
            _addPositionDataToLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(EraseLine());
            inputCount++;
        }
    }

    private void _addLineObject()
    {
        lineObj = new GameObject();
        lineObj.name = "Line";
        lineObj.AddComponent<LineRenderer>();
        lineObj.transform.SetParent(transform);
        _initRenderer();
    }

    private void _initRenderer()
    {
        lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = lineMaterial;
        lineRenderer.material.color = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    private void _addPositionDataToLineRenderer()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);


        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        lineRenderer.SetPositions(positions);
        linePoints.Add(worldPos);
    }

    private IEnumerator EraseLine()
    {
        yield return new WaitForSeconds(1);
        inputCount = 0;
        Destroy(lineObj);
    }
}
