using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScoreObjectTrigger : MonoBehaviour
{
    [SerializeField] private ScorePort scorePort;
    
    /// <summary>
    /// Based on tags "ScoreObjects"
    /// Every relevant object need ScoreObjectScript
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) //Ludel, don't complain
    {
        Debug.Log("ScoreValue: "+other.GetComponent<ScoreObject>().ScoreValue);
        scorePort.OnScore(other.GetComponent<ScoreObject>().ScoreValue);
    }
}
