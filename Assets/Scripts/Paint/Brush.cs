using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Brush : MonoBehaviour, IDragHandler, IEndDragHandler {
    [Header("Resources")]
    public PaintableTexture paintableTexture;

    [Header("Default Attributes")]
    public int radius;

    public Color color;
    private Vector2 previousPoint;

    [Header("Brush Offsets")]
    public int offsetX;

    public int offsetY;

    public void OnDrag(PointerEventData eventData) {
        var origin = new Vector2(eventData.position.x - offsetX, eventData.position.y - offsetY);
        if (!previousPoint.Equals(Vector2.zero)) {
            DrawLineBetween(previousPoint, origin);
        }

        paintableTexture.DrawCircle(origin, radius, color);
        previousPoint = origin;
        paintableTexture.UpdateTexture();
    }

    private void DrawLineBetween(Vector2 a, Vector2 b) {
        var points = GetPointsBetween(a, b, 1, 5);
        points.ForEach(point => paintableTexture.DrawCircle(point, radius, color));
    }

    private static List<Vector2> GetPointsBetween(Vector2 start, Vector2 end, float distance, float distanceThreshold) {
        var points = new List<Vector2>();
        var current = new Vector2(start.x, start.y);
        while (Vector2.Distance(current, end) >= distanceThreshold) {
            current = Vector2.MoveTowards(current, end, distance);
            points.Add(current);
        }

        return points;
    }

    public void OnEndDrag(PointerEventData eventData) {
        previousPoint = Vector2.zero;
    }
}