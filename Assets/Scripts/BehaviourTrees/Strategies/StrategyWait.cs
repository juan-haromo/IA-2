using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

public class WaitStrategy : IStrategy
{
    NavMeshAgent agent;
    float endWaitTime;

    public WaitStrategy (NavMeshAgent agent, float endWaitTime)
    {
        this.agent = agent;
        this.endWaitTime = endWaitTime;
    }
    public NodeStatus Process()
    {
        if(endWaitTime < Time.time){ return NodeStatus.Success; }
        agent.SetDestination(agent.transform.position);
        return NodeStatus.Running;
    }

    public void Reset()
    {
        //Nuh uh
    }
}