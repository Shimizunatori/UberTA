using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField,Header("線のマテリアル")] private Material lineMaterial;
    [SerializeField,Header("線の色")] private Color lineColor;
    [Range(0.1f, 0.5f)]
    [SerializeField,Header("線の幅")] private float lineWidth;
    [SerializeField] private GameObject _player;

    private Vector3 mousePos;
    private Vector3 worldPos;

    private int inputCount = 0;

    [SerializeField] private GameObject startPosObj;
    [SerializeField] private GameObject endPosObj;
    private Vector3 mousePos1;
    private Vector3 mousePos2;

    GameObject lineObj;
    public LineRenderer lineRenderer;
    public List<Vector2> linePoints;

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
            //_addPositionDataToLineRenderer();
        }
        if (Input.GetMouseButton(0))
        {
            _addPositionDataToLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _player.GetComponent<PlayerController>()._moveFlag = true;
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
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);


        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        lineRenderer.SetPositions(positions);
        linePoints.Add(worldPos);
    }

    public void RemoveVertex(int indexToRemove)
    {
        int vertexCount = lineRenderer.positionCount;
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < vertexCount; i++)
        {
            positions.Add(lineRenderer.GetPosition(i));
        }

        positions.RemoveAt(indexToRemove);

        // LineRendererに新しい頂点リストを設定
        // こうしないと次の頂点を削除できない
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    private IEnumerator EraseLine()
    {
        yield return new WaitForSeconds(1);
        inputCount = 0;
        int n = linePoints.Count;
        for (int i = 0; i <= n - 1; i++)
        {
            RemoveVertex(0);
            linePoints.RemoveAt(0);
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(lineObj);
        _player.GetComponent<PlayerController>()._moveFlag = false;
    }
}
