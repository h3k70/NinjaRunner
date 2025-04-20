using Assets.SimpleLocalization.Scripts;
using System;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public void Awake()
    {
        LocalizationManager.Read();
        LocalizationManager.Language = "Russian";

    }

    /// <summary>
    /// Change localization at runtime.
    /// </summary>
    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }
}
