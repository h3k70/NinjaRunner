using UnityEngine;

public class FPSLimit : MonoBehaviour
{
    [SerializeField] int _limit = 30;
    [SerializeField] bool _Islimit;

    private void OnValidate()
    {
        if (_Islimit)
        {
            Application.targetFrameRate = _limit;
        }
        else
        {
            Application.targetFrameRate = 0;
        }
    }

    private void Awake()
    {
        if (_Islimit)
        {
            Application.targetFrameRate = _limit;
        }
        else
        {
            Application.targetFrameRate = 0;
        }
    }
}
