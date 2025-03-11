using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;

    [SerializeField] private BarUI _HPBar;
    [SerializeField] private SourceUI _sourceUI;
    [SerializeField] private BloodFrameUI _bloodFrameUI;
    [SerializeField] private AbilityUI _baseAbilityUI;
    [SerializeField] private AbilityUI _firstAbilityUI;
    [SerializeField] private AbilityUI _secondAbilityUI;
    [SerializeField] private AbilityUI _thirdAbilityUI;

    private void Start()
    {
        _player.Init(_spawner.StartChunk.RunPoint);
        _spawner.Init(_player);

        _player.DamageTaked += _bloodFrameUI.StartHitAnim;

        _HPBar.Init(_player.Health);
        _sourceUI.Init(_spawner.Source, _player);
        _baseAbilityUI.Init(_player.BaseAttack);
        _firstAbilityUI.Init(_player.FirstSkill);
        _secondAbilityUI.Init(_player.SecondSkill);
        _thirdAbilityUI.Init(_player.ThirdSkill);
    }

    [ContextMenu("StartRun")]
    public void StartRun()
    {
        _spawner.StartSpawnChunks();
        _player.Move.StartRun();
    }
}
