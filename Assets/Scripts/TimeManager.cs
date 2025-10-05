using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float currentTime = 0;
    [SerializeField, Range(0, 200)] private int amountOfSec;
    [SerializeField] private float lowTimeLimit;

    [Space, SerializeField] private TextMeshProUGUI textMeshPro;
    
    [Space,SerializeField] private ScorePort scorePort;
    [SerializeField] private GamePort gamePort;

    private Coroutine _timerCoroutine;
    private UnityAction<uint> _scoreHandle;
    private void OnEnable()
    {
        _scoreHandle = _ => BeginTimer();
        scorePort.OnScore += _scoreHandle;
    }

    private void OnDisable()
    {
        scorePort.OnScore -= _scoreHandle;
    }

    private void BeginTimer()
    {
        Debug.Log("StartTimer");
        _timerCoroutine = StartCoroutine(HandleTime());
        scorePort.OnScore -= _scoreHandle;
    }

    private IEnumerator HandleTime()
    {
        while (currentTime < amountOfSec)
        {
            currentTime += Time.deltaTime;
            float timeLeft = amountOfSec - currentTime;

            if (timeLeft > lowTimeLimit)
            {
                WriteTimeText(Color.white, (float)Math.Round(timeLeft, 2));
            }
            else
            {
                WriteTimeText(new Color(.8f,.1f,.1f), timeLeft > 0 ? (float)Math.Round(timeLeft, 2) : 0);
            }
            yield return null;
        }
        
        gamePort.OnGameEnd(GamePort.GameStage.Victory);
        Debug.Log("EndOfTimer");
    }

    private void WriteTimeText(Color color, float timeValue)
    {
        textMeshPro.color = color;
        textMeshPro.text = timeValue.ToString();
    }
    
    //TODO event
    private void OnDestroy()
    {
        if (_timerCoroutine!= null)
        {
            StopCoroutine(_timerCoroutine);
        }
    }
}
