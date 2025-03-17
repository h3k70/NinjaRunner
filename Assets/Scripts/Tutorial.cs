using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BarUI _HPBar;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private BloodFrameUI _bloodFrameUI;

    [SerializeField] private AbilityUI _abilityBase;
    [SerializeField] private AbilityUI _abilityShuriken;
    [SerializeField] private AbilityUI _abilitySmoke;
    [SerializeField] private AbilityUI _abilityHeal;

    [SerializeField] private HandTutorialUI _handTutorialUI;
    [SerializeField] private Button _continue;
    [SerializeField] private TMP_Text _swapUpTutorText;
    [SerializeField] private TMP_Text _swapDownTutorText;
    [SerializeField] private TMP_Text _slashTutorText;
    [SerializeField] private TMP_Text _surikenTutorText;
    [SerializeField] private TMP_Text _smokeTutorText;
    [SerializeField] private TMP_Text _healTutorText;
    [SerializeField] private TMP_Text _endText;

    [SerializeField] private TutorialTrigger _swapUpTutor;
    [SerializeField] private TutorialTrigger _swapDownTutor;
    [SerializeField] private TutorialTrigger _slashTutor;
    [SerializeField] private TutorialTrigger _surikenTutor;
    [SerializeField] private TutorialTrigger _smokeTutor;
    [SerializeField] private TutorialTrigger _healTutor;

    private void Start()
    {
        _player.Init(_startChunk.RunPoint);
        _player.Move.StartRun();
        _HPBar.Init(_player.Health);
        _player.DamageTaked += _bloodFrameUI.StartHitAnim;

        _continue.onClick.AddListener(OnContinue);

        foreach (var item in _enemies)
        {
            item.Init(_player);
            item.Activate();
        }

        _abilityBase.Init(_player.BaseAttack);
        _abilityShuriken.Init(_player.ShurikenSkill);
        _abilitySmoke.Init(_player.SmokeSkill);
        _abilityHeal.Init(_player.HealSkill);

        _swapUpTutor.Triggered += OnTriggeredSwapUpTutor;
        _swapDownTutor.Triggered += OnTriggeredSwapDownTutor;
        _slashTutor.Triggered += OnTriggeredSlashTutor;
        _surikenTutor.Triggered += OnTriggeredSurikenTutor;
        _smokeTutor.Triggered += OnTriggeredSmokeTutor;
        _healTutor.Triggered += OnTriggeredHealTutor;
    }

    private void OnTriggeredSwapUpTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SwapUp();

        Time.timeScale = 0;
        _player.Move.CurrentSplineIndexChanged += SwapUpTutorEnded;

        DisableAllText();
        _swapUpTutorText.gameObject.SetActive(true);
    }

    private void SwapUpTutorEnded(int index)
    {
        if (index >= 0)
        {
            _handTutorialUI.Stop();
            _handTutorialUI.gameObject.SetActive(false);
            Time.timeScale = 1;
            _player.Move.CurrentSplineIndexChanged -= SwapUpTutorEnded;
            DisableAllText();

            _swapUpTutor.Triggered -= OnTriggeredSwapUpTutor;
        }
    }

    private void OnTriggeredSwapDownTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SwapDown();

        Time.timeScale = 0;
        _player.Move.CurrentSplineIndexChanged += SwapDownTutorEnded;

        DisableAllText();
        _swapDownTutorText.gameObject.SetActive(true);
    }

    private void SwapDownTutorEnded(int index)
    {
        if (index < 0)
        {
            _handTutorialUI.Stop();
            _handTutorialUI.gameObject.SetActive(false);
            Time.timeScale = 1;
            _player.Move.CurrentSplineIndexChanged -= SwapUpTutorEnded;
            DisableAllText();

            _swapDownTutor.Triggered -= OnTriggeredSwapDownTutor;
        }
    }

    private void OnTriggeredSlashTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SetPosition(_abilityBase.transform.position);
        _handTutorialUI.Tap();

        Time.timeScale = 0;
        _player.IsCanCast = true;
        _abilityBase.gameObject.SetActive(true);
        _player.BaseAttack.CooldownStarted += Slashed;

        DisableAllText();
        _slashTutorText.gameObject.SetActive(true);
    }

    private void Slashed(float obj)
    {
        _handTutorialUI.Stop();
        _handTutorialUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        DisableAllText();
        _player.BaseAttack.CooldownStarted -= Slashed;

        _slashTutor.Triggered -= OnTriggeredSlashTutor;
    }

    private void OnTriggeredSurikenTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SetPosition(_abilityShuriken.transform.position);
        _handTutorialUI.Tap();

        Time.timeScale = 0;
        _player.IsCanCast = true;
        _abilityShuriken.gameObject.SetActive(true);
        _player.ShurikenSkill.CooldownStarted += OnShurikenSkill;

        DisableAllText();
        _surikenTutorText.gameObject.SetActive(true);
    }

    private void OnShurikenSkill(float obj)
    {
        _handTutorialUI.Stop();
        _handTutorialUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        DisableAllText();
        _player.ShurikenSkill.CooldownStarted -= OnShurikenSkill;

        _surikenTutor.Triggered -= OnTriggeredSurikenTutor;
    }

    private void OnTriggeredSmokeTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SetPosition(_abilitySmoke.transform.position);
        _handTutorialUI.Tap();

        Time.timeScale = 0;
        _player.IsCanCast = true;
        _abilitySmoke.gameObject.SetActive(true);
        _player.SmokeSkill.CooldownStarted += OnSmokeSkill;

        DisableAllText();
        _smokeTutorText.gameObject.SetActive(true);
    }

    private void OnSmokeSkill(float obj)
    {
        _handTutorialUI.Stop();
        _handTutorialUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        DisableAllText();
        _player.SmokeSkill.CooldownStarted -= OnSmokeSkill;

        _smokeTutor.Triggered -= OnTriggeredSmokeTutor;
    }

    private void OnTriggeredHealTutor()
    {
        _handTutorialUI.gameObject.SetActive(true);
        _handTutorialUI.SetPosition(_abilityHeal.transform.position);
        _handTutorialUI.Tap();

        Time.timeScale = 0;
        _player.IsCanCast = true;
        _abilityHeal.gameObject.SetActive(true);
        _player.HealSkill.CooldownStarted += OnHeal;

        DisableAllText();
        _healTutorText.gameObject.SetActive(true);
    }

    private void OnHeal(float obj)
    {
        _handTutorialUI.Stop();
        _handTutorialUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        DisableAllText();
        _player.HealSkill.CooldownStarted -= OnSmokeSkill;

        _player.Move.StopRun();

        _healTutor.Triggered -= OnTriggeredHealTutor;

        _continue.gameObject.SetActive(true);
        _player.Animator.SetTrigger(PlayerAnimHash.Idle);
        _endText.gameObject.SetActive(true);
    }

    private void OnContinue()
    {
        SceneManager.LoadScene(0);
    }

    private void DisableAllText()
    {
        _swapUpTutorText.gameObject.SetActive(false);
        _swapDownTutorText.gameObject.SetActive(false);
        _slashTutorText.gameObject.SetActive(false);
        _surikenTutorText.gameObject.SetActive(false);
        _smokeTutorText.gameObject.SetActive(false);
        _healTutorText.gameObject.SetActive(false);
        _endText.gameObject.SetActive(false);
    }
}
