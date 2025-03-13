using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private PauseMenuUI _pauseMenuUI;
    [SerializeField] private DeadMenuUI _deadMenuUI;
    [SerializeField] private GradeMenuUI _gradeMenuUI;
    [SerializeField] private FoldingUI _foldingUI;
    [SerializeField] private float _collapseDuration;

    private void Start()
    {
        _foldingUI.Init(GetComponent<RectTransform>(), _collapseDuration);
        _pauseMenuUI.Init(_foldingUI);
        _gradeMenuUI.init(_game.Player.AllSkills, _game.Player);

        _game.RunEnded += ShowDeadMenu;

        _deadMenuUI.HomeButton.onClick.AddListener(GoHome);

        _pauseMenuUI.HomeButton.onClick.AddListener(GoHome);

        _mainMenuUI.StartButton.onClick.AddListener(StartRun);
        _mainMenuUI.GradeButton.onClick.AddListener(ShowGradeMenu);

        _gradeMenuUI.BackButton.onClick.AddListener(ShowMainMenu);

        ShowMainMenu();
    }

    private void StartRun()
    {
        _game.StartRun();
        EnablePauseMenu();
    }

    private void GoHome()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ShowMainMenu()
    {
        DisableAllMenu();
        _mainMenuUI.gameObject.SetActive(true);
        _foldingUI.Expand();
    }

    private void EnablePauseMenu()
    {
        DisableAllMenu();
        _pauseMenuUI.gameObject.SetActive(true);
        _foldingUI.Collapse();
    }

    private void ShowDeadMenu()
    {
        DisableAllMenu();
        _deadMenuUI.gameObject.SetActive(true);
        _foldingUI.Expand();
    }

    private void ShowGradeMenu()
    {
        DisableAllMenu();
        _gradeMenuUI.gameObject.SetActive(true);
        _foldingUI.Spread(_gradeMenuUI.MenuSize);
    }

    private void DisableAllMenu()
    {
        _mainMenuUI.gameObject.SetActive(false);
        _pauseMenuUI.gameObject.SetActive(false);
        _deadMenuUI.gameObject.SetActive(false);
        _gradeMenuUI.gameObject.SetActive(false);
    }
}
