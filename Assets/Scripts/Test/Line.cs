using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Line : VisualElement
{
    public Vector2 StartPoint { get; set; } = new Vector2(0, 0); // Начальная точка линии
    public Vector2 EndPoint { get; set; } = new Vector2(0, 0);  // Конечная точка линии
    public Color LineColor { get; set; } = Color.black; // Цвет линии
    public float LineWidth { get; set; } = 2f;         // Толщина линии

    public Line()
    {
        // Обеспечиваем перерисовку линии
        generateVisualContent += OnGenerateVisualContent;
    }
    public Line(Vector2 startPoint, Vector2 endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;

        // Обеспечиваем перерисовку линии
        generateVisualContent += OnGenerateVisualContent;
    }
    public Line(Vector2 startPoint, Vector2 endPoint, Color lineColor)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        LineColor = lineColor;

        // Обеспечиваем перерисовку линии
        generateVisualContent += OnGenerateVisualContent;
    }
    public Line(Vector2 startPoint, Vector2 endPoint, Color lineColor, float lineWidth)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        LineColor = lineColor;
        LineWidth = lineWidth;
        // Обеспечиваем перерисовку линии
        generateVisualContent += OnGenerateVisualContent;
    }

    private void OnGenerateVisualContent(MeshGenerationContext ctx)
    {
        var mesh = ctx.Allocate(4, 6); // 4 вершины, 6 индексов (два треугольника)

        // Вычисляем направление линии
        Vector2 direction = (EndPoint - StartPoint).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x) * (LineWidth / 2);

        // Вершины линии
        Vector3 v0 = StartPoint - perpendicular;
        Vector3 v1 = StartPoint + perpendicular;
        Vector3 v2 = EndPoint + perpendicular;
        Vector3 v3 = EndPoint - perpendicular;

        // Устанавливаем вершины
        mesh.SetNextVertex(new Vertex() { position = v0, tint = LineColor });
        mesh.SetNextVertex(new Vertex() { position = v1, tint = LineColor });
        mesh.SetNextVertex(new Vertex() { position = v2, tint = LineColor });
        mesh.SetNextVertex(new Vertex() { position = v3, tint = LineColor });

        // Устанавливаем индексы для треугольников
        mesh.SetNextIndex(0);
        mesh.SetNextIndex(1);
        mesh.SetNextIndex(2);

        mesh.SetNextIndex(2);
        mesh.SetNextIndex(3);
        mesh.SetNextIndex(0);
    }

    // Обновляем линию при изменении точек
    public void UpdateLine(Vector2 startPoint, Vector2 endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        MarkDirtyRepaint(); // Обновляем отображение
    }

    public new class UxmlFactory : UxmlFactory<Line, UxmlTraits> { }
}
