using BehaviourTrees;
using UnityEngine.AI;

public class WaitStrategy : IStrategy
{
    NavMeshAgent agent;

    public WaitStrategy (NavMeshAgent agent)
    {
        this.agent = agent;
    }
    public NodeStatus Process()
    {
        agent.SetDestination(agent.transform.position);
        return NodeStatus.Running;
    }

    public void Reset()
    {
        //Nuh uh
    }
}