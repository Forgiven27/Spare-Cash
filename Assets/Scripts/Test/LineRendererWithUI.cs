using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LineRendererWithUI : MonoBehaviour
{
    // Документ UI Toolkit
    public LineRenderer lineRenderer;  // LineRenderer для отображения линий
    public Camera mainCamera;          // Камера для преобразования координат

    private VisualElement root;         // Корневой элемент UI
    private VisualElement graphArea;    // Элемент UI для графика
    int screenHeight;
    int screenWidth;


    float graphTopMargin = 0.05f;
    float graphLeftMargin = 0.05f;

    float lineWidth = 10f;
    

    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        // Получаем корневой элемент из UI Toolkit
        root = GetComponent<UIDocument>().rootVisualElement;
        root.RegisterCallback<PointerDownEvent>(OnPointerDown);
        // Создаем область для графика

        graphArea = root.Q<VisualElement>("graph_zone");
        graphArea.style.height = screenHeight;
        graphArea.style.width = screenWidth;

        StartCoroutine(WaitForRender());

        IEnumerator WaitForRender()
        {
            yield return null; // Ждём один кадр
            Debug.Log($"Ожидание Width: {graphArea.resolvedStyle.width}, Height: {graphArea.resolvedStyle.height}");
        }
        /*
        CreateGraphLine(new Vector2(0, 0), new Vector2(210, -200));
        CreateGraphLine(new float[2] { 0, 200 }, new float[2] { 0, -200 });
        CreateGraphLine(new float[2] {0, 100}, new float[2] { 0, -300 });
        */
        CreateArrow(100, 100, 90);
        /*
        Vector2[] data = new Vector2[4]
        {
            new Vector2 (0, 0),
            new Vector2 (100, 100),
            new Vector2 (200, 100),
            new Vector2 (300, 300),
        };
        CreateGraph(data);
        */
        
    }

    void CreateAxis(float x, float y)
    {

    }

    void CreateArrow(float x, float y, float degreeOrientation)
    {
        float[] startPos = new float[2]
        {
            x,
            y
        };
        float lineHeight = 30f;
        float degree = 45;
        float x2 = x + (float)(lineHeight * Math.Cos(degree * Mathf.Deg2Rad));
        float y2 = y + (float)(lineHeight * Math.Sin(degree * Mathf.Deg2Rad));
        float[] endPos = new float[2]
        {
            x2, 
            y2
        };
        Debug.Log("x2 = " + endPos[0] + "y2 = " + endPos[1]);
        CreateGraphLine(new Vector2(startPos[0], startPos[1]), new Vector2(endPos[0], endPos[1]));
        float x3 = x + (float)(lineHeight * Math.Cos(-degree * Mathf.Deg2Rad));
        float y3 = y + (float)(lineHeight * Math.Sin(-degree * Mathf.Deg2Rad));
        float[] endPos2 = new float[2]
        {
            x3,
            y3
        };
        Debug.Log("x2 = " + endPos2[0] + "y2 = " + endPos2[1]);
        // CreateGraphLine(new Vector2(startPos[0], startPos[1]), new Vector2(endPos2[0], endPos2[1]));
    }

    void CreateGraph(Vector2[] data)
    {
        for (int i = 0; i < data.Length - 1; i += 2)
        {
            graphArea.Add(new Line(data[i], data[i + 1]));
            CreateGraphPoint(data[i]);
            CreateGraphPoint(data[i+1]);
        }
        Debug.Log("Высота " + graphArea.resolvedStyle.height);
        Debug.Log("Ширина " + graphArea.resolvedStyle.width);
    }
    

    void CreateGraphPoint(Vector2 position)
    {
        // Создаем визуальный элемент для точки
        VisualElement point = new VisualElement();
        point.style.width = 10;
        point.style.height = 10;
        point.style.backgroundColor = new StyleColor(Color.red);
        point.style.position = Position.Absolute;
        point.style.left = position.x - 5; // Центрируем точку
        point.style.top = position.y - 5;

        TextField textField = new TextField();
        textField.value = "(" + position.x + ", " + position.y + ")";
        textField.style.color = new StyleColor(Color.red);
        textField.style.position = Position.Absolute;
        textField.style.left = position.x - 5; // Центрируем точку
        textField.style.top = position.y - 5;

        // Добавляем точку в область графика
        graphArea.Add(point);
        graphArea.Add(textField);
    }

    VisualElement CreateGraphLine(float[] x, float[] y)
    {
        VisualElement line = new VisualElement();
        line.style.width = lineWidth;
        int height = Convert.ToInt32(Math.Sqrt(Math.Pow(x[0] - x[1], 2) + Math.Pow(y[0] - y[1], 2)));
        line.style.height = height;
        line.style.backgroundColor = new StyleColor(Color.black);
        line.style.position = Position.Absolute;
        
        line.style.left = x[0]; 
        line.style.top = y[0];
        
        line.style.transformOrigin = new TransformOrigin(0, 0);
        float rotationDegree = Mathf.Rad2Deg * (Mathf.Atan2(y[1] - y[0], x[1] - x[0]));
        line.transform.rotation = Quaternion.Euler(0, 0, rotationDegree);
        
        graphArea.Add(line);
        return line;
    }
    VisualElement CreateGraphLine(Vector2 startPoint, Vector2 endPoint)
    {
        VisualElement line = new VisualElement();
        line.style.width = lineWidth;
        int height = Convert.ToInt32(Math.Sqrt(Math.Pow(endPoint.x - startPoint.x, 2) + Math.Pow(endPoint.x - startPoint.y, 2)));
        line.style.height = height;
        line.style.backgroundColor = new StyleColor(Color.red);
        line.style.position = Position.Absolute;
        
        line.style.left = startPoint.x;
        line.style.top = startPoint.y;
        
        
        line.style.transformOrigin = new TransformOrigin(0, 0);
        Debug.Log($"{endPoint.y} - {startPoint.y}, {endPoint.x} - {startPoint.x}");
        float rotationDegree = Mathf.Atan2(endPoint.y - startPoint.y, endPoint.x - startPoint.x) * Mathf.Rad2Deg;
        //float rotationDegree = 45f;
        Debug.Log("Угол поворота " + rotationDegree);
        line.transform.rotation = Quaternion.Euler(0, 0, rotationDegree);
        
        graphArea.Add(line);
        return line;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        // Получаем координаты курсора в локальных координатах элемента
        Vector2 localPosition = evt.localPosition;

        // Получаем координаты курсора в глобальных координатах
        Vector2 worldPosition = evt.position;

        Debug.Log($"Click Local Position: {localPosition}, World Position: {worldPosition}");
    }

}
