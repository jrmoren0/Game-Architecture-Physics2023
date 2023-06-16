using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsController : MonoBehaviour
{
    public NavMeshAgent _agent;

    public Transform _targetPoint;

    [SerializeField] public Transform[] _targetPoints;

    [SerializeField] public Transform _agentEye;


    [SerializeField]  public float _playerCheckDistance;

    [SerializeField] public float _checkRadius = .04f;

    private EnemyState _currentState;

    public Transform _player;




    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _currentState = new EnemyIdleState(this);

        _currentState.OnStateEnter();
       

    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();  
    }

    public void ChangeStateTo(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();

    }
}


