using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScoreObjectTrigger : MonoBehaviour
{
    [SerializeField] private ScorePort scorePort;
    
    private void OnTriggerEnter(Collider other)
    {
        scorePort.OnScore(50);
    }
}
