using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private CameraFollow _cameraFollow;

    [SerializeField] private BarUI _HPBar;
    [SerializeField] private SourceUI _sourceUI;
    [SerializeField] private BloodFrameUI _bloodFrameUI;
    [SerializeField] private AbilityUI _baseAbilityUI;
    [SerializeField] private AbilityUI _firstAbilityUI;
    [SerializeField] private AbilityUI _secondAbilityUI;
    [SerializeField] private AbilityUI _thirdAbilityUI;

    private Source _source = new();
    public Player Player { get => _player; }

    public Action RunEnded;

    private void Start()
    {
        _player.Init(_spawner.StartChunk.RunPoint);
        _source.Init();
        _spawner.Init(_player, _source);

        _player.DamageTaked += _bloodFrameUI.StartHitAnim;
        _player.Died += OnPlayerDied;

        _HPBar.Init(_player.Health);
        _sourceUI.Init(_spawner.Source, _player);
        _baseAbilityUI.Init(_player.BaseAttack);
        _firstAbilityUI.Init(_player.FirstSkill);
        _secondAbilityUI.Init(_player.SecondSkill);
        _thirdAbilityUI.Init(_player.ThirdSkill);
    }

    public void StartRun()
    {
        _spawner.StartSpawnChunks();
        _player.Move.StartRun();
        _cameraFollow.TargetMoveTransform = null;
    }

    private void OnPlayerDied()
    {
        StartCoroutine(PlayerDiedJob());
    }

    private IEnumerator PlayerDiedJob()
    {
        _cameraFollow.TargetMoveTransform = _player.DieCameraPoint;
        yield return new WaitForSecondsRealtime(1.5f);
        RunEnded?.Invoke();
    }
}
