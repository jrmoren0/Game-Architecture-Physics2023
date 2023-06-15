using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{

    float distancetoPlayer;


    public EnemyFollowState(AgentsController enemy): base(enemy)
    {

    }


    public override void OnStateEnter()
    {
        Debug.Log("Enemy will follow now");
    }


    public override void OnStateUpdate()
    {
     if(_enemy._player != null)
        {
            distancetoPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (distancetoPlayer > 10)
            {
                _enemy.ChangeStateTo(new EnemyIdleState(_enemy));
            }
            if(distancetoPlayer < 2)
            {
                _enemy.ChangeStateTo(new EnemyAttackState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            _enemy.ChangeStateTo(new EnemyIdleState(_enemy));
        }
    }



    public override void OnStateExit()
    {
        Debug.Log("Enemy will NOT follow now");
    }



   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
