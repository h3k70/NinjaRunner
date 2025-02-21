using UnityEngine;
using UnityEngine.Splines;

public class Build : MonoBehaviour
{
    [SerializeField] private SplineContainer[] _splineContainers;
    [SerializeField] private Enemy[] _enemys;
    [SerializeField] private int _maxEnemy;
    [SerializeField] private Build _nextBuild;

    public virtual Build NextBuild { get => _nextBuild; set => _nextBuild = value; }

    public virtual SplineContainer[] SplineContainers => _splineContainers;
}
