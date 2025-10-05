using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MischiefEvent : ScriptableObject
{
    public bool allowMischief;
    [HideInInspector] public UnityEvent OnMischief= new UnityEvent();
    public GameObject interactableObject;
}
