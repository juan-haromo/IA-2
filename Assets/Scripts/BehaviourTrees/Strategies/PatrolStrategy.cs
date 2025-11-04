using System.Collections.Generic;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStrategy : IStrategy
{
    public Transform entity;
    public NavMeshAgent agent;
    public List<Transform> patrolPoints;
    public float patrolSpeed;
    public int currentIndex;

    private bool isPathCalculated;

    public PatrolStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed)
    {
        this.entity = entity;
        this.agent = agent;
        this.patrolPoints = patrolPoints;
        this.patrolSpeed = patrolSpeed;
    }

    public NodeStatus Process()
    {
        if (currentIndex == patrolPoints.Count)
        {
            return NodeStatus.Success;
        }

        Transform target = patrolPoints[currentIndex];
        agent.SetDestination(target.position);
        entity.LookAt(new Vector3(target.position.x, entity.position.y, target.position.z));

        if (isPathCalculated && agent.remainingDistance < 0.1f)
        {
            isPathCalculated = false;
            currentIndex++;
            //currentIndex %= patrolPoints.Count;
        }
        if (agent.pathPending)
        {
            isPathCalculated = true;
        }
        return NodeStatus.Running;
    }

    public void Reset()
    {
        currentIndex = 0;
    }
}