using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

public class StrategyWanderAround : IStrategy
{
    bool arrived;
    Transform pointToWander;
    float wanderRadius;
    NavMeshAgent agent;
    bool findPoint;
    public StrategyWanderAround(Transform pointToWander, float wanderRadius, NavMeshAgent agent)
    {
        this.pointToWander = pointToWander;
        this.wanderRadius = wanderRadius;
        this.agent = agent;
        arrived = true;
    }

    public NodeStatus Process()
    {
        if (!findPoint)
        {
            agent.SetDestination(FindPoint(pointToWander.position));
            findPoint = true;
        }
        if (arrived)
        {
            return NodeStatus.Success;
        }
        arrived = agent.remainingDistance < 0.5f;
        return NodeStatus.Running;
    }

    public void Reset()
    {
        arrived = false;
        findPoint = false;
    }

    Vector3 FindPoint(Vector3 startPos)
    {
        Vector3 randomPoint = startPos + (Random.insideUnitSphere * wanderRadius);

        NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas);
        return hit.position;
    } 
}