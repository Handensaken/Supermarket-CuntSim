using System;
using UnityEngine;

public class MischiefHandler : MonoBehaviour
{
    [SerializeField] private MischiefEvent mischiefEvent;

    private void OnTriggerEnter(Collider other)
    {
        mischiefEvent.allowMischief = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mischiefEvent.allowMischief = false;
    }
}
