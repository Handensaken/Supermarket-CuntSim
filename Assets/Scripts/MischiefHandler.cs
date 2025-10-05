using System;
using UnityEngine;

public class MischiefHandler : MonoBehaviour
{
    [SerializeField] private MischiefEvent mischiefEvent;
    [SerializeField] private GameObject interactableObject;

    private void Awake()
    {
        mischiefEvent.allowMischief = false;
        mischiefEvent.interactableObject = interactableObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        mischiefEvent.allowMischief = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        mischiefEvent.allowMischief = false;
    }
}
