using System;
using UnityEngine;

public class MischiefHandler : MonoBehaviour
{
    [SerializeField] private MischiefEvent mischiefEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player") return;
        mischiefEvent.allowMischief = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Player") return;
        mischiefEvent.allowMischief = false;
    }
}
