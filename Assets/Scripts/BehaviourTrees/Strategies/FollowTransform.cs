using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

public class FollowTransform : IStrategy
{

    Transform targetToFollow;
    NavMeshAgent agent;
    float nextFollowTime;

    public FollowTransform(Transform targetToFollow, NavMeshAgent agent)
    {
        this.targetToFollow = targetToFollow;
        this.agent = agent;
    }

    public NodeStatus Process()
    {
        //Stop agent when to close
        if (Vector3.Distance(agent.transform.position, targetToFollow.transform.position) < 2.5f)
        {
            agent.SetDestination(agent.transform.position);
            return NodeStatus.Success;
        }
        //Update path every 0.1s
        if (nextFollowTime < Time.time)
        {
            nextFollowTime += 0.1f;
            agent.SetDestination(targetToFollow.position);
        }
        return NodeStatus.Running;
    }

    public void Reset()
    {
        
    }
}