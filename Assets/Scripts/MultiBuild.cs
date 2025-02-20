using UnityEngine;
using UnityEngine.Splines;

public class MultiBuild : Build
{
    [SerializeField] private Build[] _builds;

    public override Build NextBuild { get => _builds[1]; set => _builds[^1] = value; }
    public override SplineContainer[] SplineContainers => _builds[0].SplineContainers;
}
