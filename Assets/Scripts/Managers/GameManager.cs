using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private LevelManager[] _levels;


    public static GameManager _instance;


    private Gamestate _currentState;

    private LevelManager _currentLevel;

    private int _currentLevelIndex = 0;

    private bool _isInputActive = true;


    //Singeloton Pattern
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


    public static GameManager GetInstance()
    {
        return _instance;
    }


    


    public bool IsInputActive()
    {
        return _isInputActive;
    }


    // Start is called before the first frame update
    void Start()
    {

        if(_levels.Length > 0)
        {
            ChangeStateTo(Gamestate.Briefing, _levels[_currentLevelIndex]);
        }

    }

    public void ChangeStateTo(Gamestate state, LevelManager level)
    {
        _currentState = state;
        _currentLevel = level;


        switch (_currentState)
        {

            case Gamestate.Briefing:
                StartBriefing();
                break;
            case Gamestate.LevelStart:
                InitiateLevel();
                break;
            case Gamestate.LevelIn:
                RunLevel();
                break; 
            case Gamestate.LevelEnd:
                CompletedLevel();
                break;
            case Gamestate.GamerOver:
                GameOver();
                break;
            case Gamestate.GameWin:
                GameWin();
                break;

        }



    }


    private void StartBriefing()
    {
        Debug.Log("Briefing Started!");

        _isInputActive = false;

        //TODO
        //set start state


        ChangeStateTo(Gamestate.LevelStart, _currentLevel);

    }

    private void InitiateLevel()
    {
        Debug.Log("Level Start");

        _isInputActive = true;



        _currentLevel.StartLevel();
        ChangeStateTo(Gamestate.LevelIn, _currentLevel);


    }

    private void RunLevel()
    {
        Debug.Log("Running Level " + _currentLevel.gameObject.name);
    }

    public void CompletedLevel()
    {
        Debug.Log("Level End");

        //goes to next level

        if (_currentLevel._isFinalLevel)

        {
            ChangeStateTo(Gamestate.GameWin, _currentLevel);
        }
        else
        {


            ChangeStateTo(Gamestate.LevelStart, _levels[++_currentLevelIndex]);

        }

    }


    private void GameOver()
    {
        Debug.Log("Game Over, You Lose");
    }

    private void GameWin()
    {
        Debug.Log("Game Over, You Win!");
    }


    public enum Gamestate
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GamerOver,
        GameWin,

    }


}
