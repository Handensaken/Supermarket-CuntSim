using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "Mischief Events/ Start Fire")]
public class StartFire : MischiefEvent
{
    private void OnEnable()
    {
        OnMischief.AddListener(Mischief);
    }

    private void OnDisable()
    {
        OnMischief.RemoveListener(Mischief);
    }
    
    private void Mischief()
    {
        interactableObject.SetActive(true);
    }
}
