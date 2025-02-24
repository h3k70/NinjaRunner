using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunksPull;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private Player _player;

    private Queue<Chunk> _chunksQueue;
    private Chunk _currentChunk;
    private Chunk _nextChunk;
    private float _chunkSizeZ = 40;
    private float _chunkSizeX = 100;

    private void Update()
    {
        if(_player.transform.position.x - _nextChunk.RunPoint.position.x >= 0)
        {
            _chunksPull.Add(_currentChunk);
            _currentChunk = _nextChunk;

            if (_chunksQueue.TryDequeue(out Chunk newChunk))
            {
                _nextChunk = newChunk;
            }
            else
            {
                GenerateQueueOfChunks();
                _nextChunk = _chunksQueue.Dequeue();
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
