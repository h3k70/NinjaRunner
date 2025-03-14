using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioToggleUI : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    public void SetAudio(bool b)
    {
        AudioListener.volume = !b ? 1 : 0;
    }

    private void OnEnable()
    {
        if (AudioListener.volume == 0)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }
}
