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
        if (!interactableObject.GetComponent<MischiefHandler>()) return;
        MischiefHandler mischief = interactableObject.GetComponent<MischiefHandler>();
        mischief.enabled = false;
        
        interactableObject.SetActive(true);
    }
}
