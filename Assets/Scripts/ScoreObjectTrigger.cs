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
        ScoreObject scoreObject = other.GetComponent<ScoreObject>();
        if (scoreObject.enabled)
        {
            scorePort.OnScore(scoreObject.ScoreValue);
            scoreObject.enabled = false;
        }
        
    }
}
