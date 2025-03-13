using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunksPull;
    [SerializeField] private Chunk _startChunk;

    private Queue<Chunk> _chunksQueue = new();
    private Chunk _currentChunk;
    private Chunk _nextChunk;
    private float _chunkSizeZ = 40;
    private float _chunkSizeX = 100;
    private Player _player;
    private Source _source;
    private float _rewardSourcePointForKilling = 20;
    private Coroutine _swapnChunkCoroutine;
    private int _rewardSourceCoinForKilling = 10;

    public Source Source { get => _source; }
    public Chunk StartChunk { get => _startChunk; }

    public void Init(Player player, Source source)
    {
        _player = player;
        _source = source;

        OnLVLChanged(_source.CurrentLVL);
        _source.LVLChanged += OnLVLChanged;

        foreach (var item in _chunksPull)
        {
            item.Init(_player, _source);
        }

        foreach (Chunk chunk in _chunksPull)
        {
            foreach (Enemy enemy in chunk.Enemies)
            {
                enemy.Die += OnEnemyDie;
            }
        }
    }

    public void StartSpawnChunks()
    {
        GenerateQueueOfChunks();

        _startChunk.Init(_player, _source);
        _startChunk.Activate();

        _currentChunk = _chunksQueue.Dequeue();
        _currentChunk.transform.position += _startChunk.EndConnectPoint.position - _currentChunk.StartConnectPoint.position;
        _currentChunk.Activate();

        _nextChunk = _chunksQueue.Dequeue();
        _nextChunk.transform.position += _currentChunk.EndConnectPoint.position - _nextChunk.StartConnectPoint.position;
        _nextChunk.Activate();

        _currentChunk.Builds[^1].NextBuild = _nextChunk.Builds[0];
        _swapnChunkCoroutine = StartCoroutine(SwapnChunkJob());
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

    private IEnumerator SwapnChunkJob()
    {
        while (true)
        {
            if (_player.transform.position.x - _nextChunk.RunPoint.position.x >= 0)
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
            yield return null;
        }
    }
}
