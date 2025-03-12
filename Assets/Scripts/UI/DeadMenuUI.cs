using UnityEngine;
using UnityEngine.UI;

public class DeadMenuUI : MonoBehaviour
{
    [SerializeField] private Button _homeButton;

    public Button HomeButton { get => _homeButton; set => _homeButton = value; }
}
