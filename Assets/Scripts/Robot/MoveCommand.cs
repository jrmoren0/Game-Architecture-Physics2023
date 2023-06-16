using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent _agent;

    private Vector3 _destination;


    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        this._agent = agent;
        this._destination = destination;

    }

    public override bool _isComplete => ReachedDestination();


    public override void Execute()
    {
        _agent.SetDestination(_destination);
    }

    public override void Undo()
    {
        _agent.SetDestination(_destination);
    }



    private bool ReachedDestination()
    {
        if(_agent.remainingDistance > 0.1f)
        {
            return false;
        }



        return true;
    }
}
