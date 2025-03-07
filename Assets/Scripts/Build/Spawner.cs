using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunksPull;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private Player _player;

    [SerializeField] private BarUI _HPBar;
    [SerializeField] private SourceUI _sourceUI;
    [SerializeField] private BloodFrameUI _bloodFrameUI;
    [SerializeField] private AbilityUI _baseAbilityUI;
    [SerializeField] private AbilityUI _firstAbilityUI;
    [SerializeField] private AbilityUI _secondAbilityUI;
    [SerializeField] private AbilityUI _thirdAbilityUI;

    private Queue<Chunk> _chunksQueue = new();
    private Chunk _currentChunk;
    private Chunk _nextChunk;
    private float _chunkSizeZ = 40;
    private float _chunkSizeX = 100;

    private Source _source = new();
    private float _rewardSourcePointForKilling = 20;
    private float _maxStartSource = 100;
    private float _deleyForSourcePointAdd = 1;
    private float _pointCountForSourceAdd = 5;
    private Coroutine _sourcePointAddCoroutine;

    private int _rewardSourceCoinForKilling = 10;

    private void Start()
    {
        _player.Init(_startChunk.RunPoint);

        foreach (var item in _chunksPull)
        {
            item.Init(_player);
        }

        foreach (Chunk chunk in _chunksPull)
        {
            foreach (Enemy enemy in chunk.Enemies)
            {
                enemy.Die += OnEnemyDie;
            }
        }
        GenerateQueueOfChunks();

        _startChunk.Init(_player);
        _startChunk.Activate();

        _currentChunk = _chunksQueue.Dequeue();
        _currentChunk.transform.position += _startChunk.EndConnectPoint.position - _currentChunk.StartConnectPoint.position;
        _currentChunk.Activate();

        _nextChunk = _chunksQueue.Dequeue();
        _nextChunk.transform.position += _currentChunk.EndConnectPoint.position - _nextChunk.StartConnectPoint.position;
        _nextChunk.Activate();

        _currentChunk.Builds[^1].NextBuild = _nextChunk.Builds[0];
        //-----------------------------------------------------------------------------------
        _HPBar.Init(_player.Health);
        _player.DamageTaked += _bloodFrameUI.StartHitAnim;

        _source.Init();
        OnLVLChanged(_source.CurrentLVL);
        _sourcePointAddCoroutine = StartCoroutine(AddSourcePointJob());
        _source.LVLChanged += OnLVLChanged;

        _sourceUI.Init(_source, _player);

        _baseAbilityUI.Init(_player.BaseAttack);
        _firstAbilityUI.Init(_player.FirstSkill);
        _secondAbilityUI.Init(_player.SecondSkill);
        _thirdAbilityUI.Init(_player.ThirdSkill);
    }

    private void Update()
    {
        if(_player.transform.position.x - _nextChunk.RunPoint.position.x >= 0)
        {
            _chunksPull.Add(_currentChunk);
            _currentChunk.Deactivate();

            _currentChunk = _nextChunk;

            if (_chunksQueue.TryDequeue(out Chunk newChunk))
            {
                _nextChunk = newChunk;
                _nextChunk.transform.position += _currentChunk.EndConnectPoint.position - _nextChunk.StartConnectPoint.position;
                _nextChunk.Activate();
            }
            else
            {
                GenerateQueueOfChunks();
                _nextChunk = _chunksQueue.Dequeue();
                _nextChunk.transform.position += _currentChunk.EndConnectPoint.position - _nextChunk.StartConnectPoint.position;
                _nextChunk.Activate();
            }
            _currentChunk.Builds[^1].NextBuild = _nextChunk.Builds[0];
        }
    }

    private void GenerateQueueOfChunks()
    {
        Shuffle(_chunksPull);

        foreach (var item in _chunksPull)
            _chunksQueue.Enqueue(item);

        _chunksPull.Clear();
    }

    private void Shuffle<T>(IList<T> array)
    {
        int n = array.Count;

        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    private void OnLVLChanged(int lvl)
    {
        _player.Move.Speed = _player.Move.DefaultSpeed * (lvl * 0.1f + 1);
    }

    private void OnEnemyDie()
    {
        _source.Add(_rewardSourcePointForKilling);
        _player.AddCoin((int)(_rewardSourceCoinForKilling * (_source.CurrentLVL * 0.1f + 1)));
    }

    private IEnumerator AddSourcePointJob()
    {
        while (true)
        {
            yield return new WaitForSeconds(_deleyForSourcePointAdd);
            _source.Add(_pointCountForSourceAdd);
        }
    }
}
