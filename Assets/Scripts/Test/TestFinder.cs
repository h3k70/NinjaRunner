using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TestFinder : MonoBehaviour
{
    public SplineContainer splineContainer; // Контейнер сплайна
    public Transform target; // Целевая позиция (например, объект в сцене)

    void Update()
    {
        if (splineContainer != null && splineContainer.Spline != null && target != null)
        {
            Spline spline = splineContainer.Spline;

            // 1. Находим ближайшую точку на сплайне и её параметр t
            FindNearestPointOnSpline(splineContainer, target.position, out Vector3 nearestPoint, out float t);

            // Визуализация
            Debug.DrawLine(target.position, splineContainer.transform.TransformPoint(nearestPoint), Color.red); // Линия к ближайшей точке

            Debug.DrawLine(target.position, splineContainer.transform.TransformPoint(spline.EvaluatePosition(1)), Color.green); // Линия от ближайшей точки к точке на сплайне
            //Debug.Log("Параметр t: " + t);
        }
    }

    // Метод для поиска ближайшей точки на сплайне и её параметра t
    private void FindNearestPointOnSpline(SplineContainer splineContainer, Vector3 targetPosition, out Vector3 nearestPoint, out float nearestT)
    {
        Spline spline = splineContainer.Spline;
        targetPosition = splineContainer.transform.InverseTransformPoint(targetPosition);
        nearestPoint = Vector3.zero;
        nearestT = 0f;
        float closestDistance = float.MaxValue;
        int steps = 50; // Количество шагов для поиска

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector3 pointOnSpline = spline.EvaluatePosition(t);
            float distance = (targetPosition - pointOnSpline).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestPoint = pointOnSpline;
                nearestT = t;
            }
        }
    }

    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<SplineContainer> _splineContainer;

    private SplineContainer _currentSpline;
    private int AccuracyFindingNearestSplineContainer = 3;



    private SplineContainer FindNearestSplineContainer(List<SplineContainer> splineContainers, out Vector3 point)
    {
        SplineContainer nearesObject = null;
        float closestDistance = float.MaxValue;
        point = Vector3.zero;

        foreach (var item in splineContainers)
        {
            FindNearestPointOnSpline(item, out Vector3 position, out float t, AccuracyFindingNearestSplineContainer);

            float distance = (position - transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearesObject = item;
                point = position;
            }
        }
        return nearesObject;
    }

    private void FindNearestPointOnSpline(SplineContainer splineContainer, out Vector3 nearestPoint, out float nearestT, int step = 50)
    {
        Spline spline = splineContainer.Spline;
        Vector3 targetPosition = splineContainer.transform.InverseTransformPoint(transform.position);
        nearestPoint = Vector3.zero;
        nearestT = 0f;
        float closestDistance = float.MaxValue;

        for (int i = 0; i <= step; i++)
        {
            float t = i / (float)step;
            Vector3 pointOnSpline = spline.EvaluatePosition(t);
            float distance = (targetPosition - pointOnSpline).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestPoint = pointOnSpline;
                nearestT = t;
            }
        }
        nearestPoint = splineContainer.transform.TransformPoint(nearestPoint);
    }
}
