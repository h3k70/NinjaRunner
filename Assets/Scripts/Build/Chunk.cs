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
    private float _activationChance = 50f;

    public Transform StartConnectPoint { get => _startConnectPoint; }
    public Transform EndConnectPoint { get => _endConnectPoint; }
    public Transform RunPoint { get => _runPoint; }
    public Player Player { get => _player; }
    public Build[] Builds { get => _builds; set => _builds = value; }
    public Enemy[] Enemies { get => _enemies; }

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

        foreach (var item in _enemies)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void ResetEnemy()
    {
        foreach (var item in _enemies)
        {
            float randomValue = Random.Range(0f, 100f);

            if (randomValue <= _activationChance)
            {
                item.Init(_player);
                item.Activate();
            }
        }
    }
}
