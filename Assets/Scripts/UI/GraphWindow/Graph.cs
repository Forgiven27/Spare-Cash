using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Graph : MonoBehaviour
{
    public GameObject pointPrefab; // Префаб точки
    public Material lineMaterial; // Материал для линии
    public float pointSize = 100f; // Размер точек
    public float lineWidth = 600f; // Ширина линии

    private LineRenderer lineRenderer;
    private RectTransform canvasRect;

    void Start()
    {
        // Получаем RectTransform родительского Canvas
        /*
        canvasRect = GetParentCanvasRect();

        if (canvasRect != null)
        {
            // Создаем LineRenderer
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // Пример данных для графика
            Vector2[] points = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 2),
                new Vector2(2, 1),
                new Vector2(3, 3),
                new Vector2(4, 0)
            };

            // Рисуем оси X и Y
            DrawAxes();

            // Рисуем точки и линии
            DrawPointsAndLines(points);
        }
        else
        {
            Debug.LogError("Parent Canvas not found!");
        }*/
    }

    RectTransform GetParentCanvasRect()
    {
        // Находим родительский Canvas
        Transform parent = transform.parent;
        while (parent != null)
        {
            Canvas canvas = parent.GetComponent<Canvas>();
            if (canvas != null)
            {
                return canvas.GetComponent<RectTransform>();
            }
            parent = parent.parent;
        }
        return null;
    }

    void DrawAxes()
    {
        // Создаем LineRenderer для осей
        LineRenderer xAxis = new GameObject("X Axis").AddComponent<LineRenderer>();
        LineRenderer yAxis = new GameObject("Y Axis").AddComponent<LineRenderer>();

        xAxis.material = lineMaterial;
        yAxis.material = lineMaterial;
        xAxis.startWidth = lineWidth;
        xAxis.endWidth = lineWidth;
        yAxis.startWidth = lineWidth;
        yAxis.endWidth = lineWidth;

        // Получаем размер Canvas
        Vector2 canvasSize = canvasRect.sizeDelta;

        // Рисуем ось X
        xAxis.positionCount = 2;
        xAxis.SetPosition(0, new Vector3(0, canvasSize.y, 0));
        xAxis.SetPosition(1, new Vector3(canvasSize.x, canvasSize.y, 0));

        // Рисуем ось Y
        yAxis.positionCount = 2;
        yAxis.SetPosition(0, new Vector3(0, canvasSize.y, 0));
        yAxis.SetPosition(1, new Vector3(0, 0, 0));
    }

    void DrawPointsAndLines(Vector2[] points)
    {
        // Получаем размер Canvas
        Vector2 canvasSize = canvasRect.sizeDelta;

        // Преобразуем точки в координаты экрана
        Vector3[] screenPoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            screenPoints[i] = new Vector3(points[i].x, canvasSize.y - points[i].y, 0);
        }

        // Рисуем точки
        foreach (Vector3 point in screenPoints)
        {
            GameObject pointObject = Instantiate(pointPrefab, point, Quaternion.identity);
            pointObject.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
        }

        // Рисуем линии
        lineRenderer.positionCount = screenPoints.Length;
        lineRenderer.SetPositions(screenPoints);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OnBackButtonPressed();
        }
    }
    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
