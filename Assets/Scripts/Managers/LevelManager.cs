using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{

    [SerializeField] public bool _isFinalLevel;

    public UnityEvent _onLevelStart;

    public UnityEvent _onLevelEnd;


    public void StartLevel()
    {
        _onLevelStart?.Invoke();
    }


    public void EndLevel()
    {
        _onLevelEnd?.Invoke();
    }
}
