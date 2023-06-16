using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    NavMeshAgent _agent;


    private Builder _builder;


    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        this._agent = agent;
        this._builder = builder;
    }

    

    public override bool _isComplete => BuildComplete();

    public override void Execute()
    {
        _agent.SetDestination(_builder.transform.position);
    }

    public override void Undo()
    {
        _agent.SetDestination(_builder.transform.position);
    }

    bool BuildComplete()
    {
        if (_agent.remainingDistance > .01f)
        {
            return false;
        }

        if (_builder != null)
        {
            _builder.Build();
        }
        
        return true;
    }

}
