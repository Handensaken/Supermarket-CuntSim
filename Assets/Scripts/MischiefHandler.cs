using System;
using UnityEngine;

public class MischiefHandler : MonoBehaviour
{
    [SerializeField] private MischiefEvent mischiefEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        mischiefEvent.allowMischief = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("out");
        mischiefEvent.allowMischief = false;
    }
}
