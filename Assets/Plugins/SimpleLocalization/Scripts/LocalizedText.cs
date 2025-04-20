using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization.Scripts
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;

        private TMP_Text _TMPText;

        public void Start()
        {
            _TMPText = GetComponent<TMP_Text>();
            Localize();
            LocalizationManager.OnLocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.OnLocalizationChanged -= Localize;
        }

        private void Localize()
        {
            _TMPText.text = LocalizationManager.Localize(LocalizationKey);
        }
    }
}