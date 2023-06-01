using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agents : MonoBehaviour
{
    public Transform _targetPoint;
    NavMeshAgent _agent;

    private void Start()
    {
      _agent = GetComponent<NavMeshAgent>();
      

    }


    private void Update()
    {
        _agent.destination = _targetPoint.position;
    }

}
