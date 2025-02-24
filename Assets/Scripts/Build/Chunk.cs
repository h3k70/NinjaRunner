using UnityEngine;
using UnityEngine.Splines;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Build[] _builds;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform _runPoint;
    [SerializeField] private Transform _startConnectPoint;
    [SerializeField] private Transform _endConnectPoint;

    public Transform StartConnectPoint { get => _startConnectPoint; }
    public Transform EndConnectPoint { get => _endConnectPoint; }
    public Transform RunPoint { get => _runPoint; }

#if UNITY_EDITOR
    [ContextMenu("connectBuild")]
    private void ConnectBuild()
    {
        for (int i = 0; i < _builds.Length - 1; i++)
        {
            _builds[i].NextBuild = _builds[i + 1];
        }
    }
#endif

    public void ResetEnemy()
    {
        
    }
}
