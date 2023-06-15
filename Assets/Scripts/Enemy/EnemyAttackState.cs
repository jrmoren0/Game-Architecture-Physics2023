using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState

{
    float _distanceToPlayer;

    Health _playerHealth;


    float _damagePerSecond = 5f;



public EnemyAttackState(AgentsController enemy) : base(enemy)
    {

        _playerHealth = enemy._player.GetComponent<Health>();
}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public override void OnStateEnter()
{
        Debug.Log("Enemy is starting Attack");
}

public override void OnStateUpdate()
{
        Attack();

        _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
        if (_enemy._player != null)
        {

            if (_distanceToPlayer > 2)
            {
                _enemy.ChangeStateTo(new EnemyFollowState(_enemy));
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
        Debug.Log("Enemy no longer Attacking");
    
}


    void Attack(){
        if(_playerHealth != null)
        {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
        }
    }

}
