using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MischiefEvent : ScriptableObject
{
    [HideInInspector] public bool allowMischief;
    [HideInInspector] public UnityEvent OnMischief= new UnityEvent();
}
