using UnityEngine;
using UnityEngine.Splines;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Build[] _builds;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform _runPoint;
    [SerializeField] private Transform _startConnectPoint;
    [SerializeField] private Transform _endConnectPoint;

    private Player _player;

    public Transform StartConnectPoint { get => _startConnectPoint; }
    public Transform EndConnectPoint { get => _endConnectPoint; }
    public Transform RunPoint { get => _runPoint; }
    public Player Player { get => _player; }

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

    public void Init(Player player)
    {
        _player = player;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        ResetEnemy();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void ResetEnemy()
    {
        foreach (var item in _builds)
        {
            item.ResetEnemy();
        }

        foreach (var item in _enemies)
        {
            item.Init(_player);
            item.Activate();
        }
    }
}
