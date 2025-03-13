using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private PauseMenuUI _pauseMenuUI;
    [SerializeField] private DeadMenuUI _deadMenuUI;
    [SerializeField] private GradeUI _gradeUI;
    [SerializeField] private FoldingUI _foldingUI;
    [SerializeField] private float _collapseDuration;

    private void Start()
    {
        _foldingUI.Init(GetComponent<RectTransform>(), _collapseDuration);
        _pauseMenuUI.Init(_foldingUI);

        _game.RunEnded += ShowDeadMenu;

        _deadMenuUI.HomeButton.onClick.AddListener(GoHome);

        _pauseMenuUI.HomeButton.onClick.AddListener(GoHome);

        _mainMenuUI.StartButton.onClick.AddListener(StartRun);
    }

    private void StartRun()
    {
        _game.StartRun();
        DisableAllMenu();
        _pauseMenuUI.gameObject.SetActive(true);
        _foldingUI.Collapse();
    }

    private void GoHome()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ShowDeadMenu()
    {
        DisableAllMenu();
        _deadMenuUI.gameObject.SetActive(true);
        _foldingUI.Expand();
    }

    private void DisableAllMenu()
    {
        _mainMenuUI.gameObject.SetActive(false);
        _pauseMenuUI.gameObject.SetActive(false);
        _deadMenuUI.gameObject.SetActive(false);
        _gradeUI.gameObject.SetActive(false);
    }
}
