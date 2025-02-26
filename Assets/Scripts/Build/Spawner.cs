using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunksPull;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private Player _player;

    private Queue<Chunk> _chunksQueue = new();
    [SerializeField] private Chunk _currentChunk;
    [SerializeField] private Chunk _nextChunk;
    private float _chunkSizeZ = 40;
    private float _chunkSizeX = 100;

    private void Start()
    {
        foreach (var item in _chunksPull)
        {
            item.Init(_player);
        }
        _currentChunk.Init(_player);
        _currentChunk.Activate();
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
}
