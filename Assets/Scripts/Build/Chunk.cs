using UnityEngine;
using UnityEngine.Splines;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Build[] _builds;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform _runPoint;

    private SplineContainer[] _allSplineContainers;

    public Transform RunPoint { get => _runPoint; }
    public SplineContainer[] AllSplineContainers { get => _allSplineContainers; set => _allSplineContainers = value; }
}
