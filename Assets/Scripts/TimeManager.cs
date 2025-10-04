using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float time = 0;
    [SerializeField, Range(0, 200)] private int amountOfSec;
    
    [Space,SerializeField] private ScorePort scorePort;
    [Space,SerializeField] private GamePort gamePort;

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
        while (time < amountOfSec)
        {
            time += Time.deltaTime;
            yield return null;
        }
        gamePort.onGameTimeEnd();
        Debug.Log("EndOfTimer");
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
