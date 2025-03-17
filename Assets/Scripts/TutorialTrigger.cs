using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Action Triggered;

    private void OnTriggerEnter(Collider other)
    {
        Triggered?.Invoke();
    }
}
