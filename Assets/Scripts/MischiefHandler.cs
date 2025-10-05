using System;
using UnityEngine;

public class MischiefHandler : MonoBehaviour
{
    [SerializeField] private MischiefEvent mischiefEvent;
    [SerializeField] private GameObject interactableObject;

    private void OnTriggerEnter(Collider other)
    {
        mischiefEvent.interactableObject = interactableObject;
        mischiefEvent.allowMischief = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mischiefEvent.allowMischief = false;
    }
}
