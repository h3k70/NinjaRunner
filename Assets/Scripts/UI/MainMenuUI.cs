using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _gradeButton;
    [SerializeField] private Button _leadersButton;

    public Button StartButton { get => _startButton; }
    public Button GradeButton { get => _gradeButton; }
    public Button LeadersButton { get => _leadersButton; }
}
