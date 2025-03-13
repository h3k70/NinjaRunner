using UnityEngine;
using UnityEngine.UI;

public class GradeMenuUI : MonoBehaviour
{
    [SerializeField] private Vector2 _menuSize;
    [SerializeField] private Button _backButton;
    [SerializeField] private UpgradeButtonUI[] _upgradeButtons;

    public Button BackButton { get => _backButton; }
    public Vector2 MenuSize { get => _menuSize; }

    public void Init(IGradable[] gradables, Player player)
    {
        for (int i = 0; i < gradables.Length; i++)
        {
            _upgradeButtons[i].Init(gradables[i], player);
        }
    }
}
