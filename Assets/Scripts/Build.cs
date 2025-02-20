using UnityEngine;
using UnityEngine.Splines;

public class Build : MonoBehaviour
{
    [SerializeField] private SplineContainer[] _splineContainers;
    [SerializeField] private Transform[] _enemySpawnPoints;

    [SerializeField] private Build _nextBuild;

    public virtual Build NextBuild { get => _nextBuild; set => _nextBuild = value; }

    public virtual SplineContainer[] SplineContainers => _splineContainers;
}
