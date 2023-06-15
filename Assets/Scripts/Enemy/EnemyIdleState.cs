using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{

    int _currentTarget = 0;

    public EnemyIdleState(AgentsController enemy): base(enemy)
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnStateEnter()
    {
        _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        Debug.Log("Enter IDLING");
    }

    public override void OnStateExit()
    {
        Debug.Log(" Exit Idling");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._agent.remainingDistance < 0.1f)
        {

            _currentTarget++;
            if (_currentTarget >= _enemy._targetPoints.Length)
            {
                _currentTarget = 0;

            }
            _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemy._agentEye.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found!!!!");

                _enemy._player = hit.transform;
                _enemy._agent.destination = _enemy._player.position;



                _enemy.ChangeStateTo(new EnemyFollowState(_enemy));
              

            }
        }

    }
}
