using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private uint currentScore = 0;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    
    [Space, SerializeField] private ScorePort scorePort;
    private void OnEnable()
    {
        scorePort.OnScore += AddPoints;
    }

    private void OnDisable()
    {
        scorePort.OnScore -= AddPoints;
    }

    private void AddPoints(uint addedValue)
    {
        currentScore += addedValue;
        textMeshProUGUI.text = "Score: " + currentScore;
    }
}
