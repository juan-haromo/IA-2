
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MoveToPoint",menuName = "FSM/States/MoveToPoint")]
public class MoveToPoint : State
{
    public  string pointName = "A";

    public override void Enter(StateMachine stateMachine)
    {
        GetPoint(stateMachine);
    }

    private void GetPoint(StateMachine stateMachine)
    {
        Vector3 destination;
        destination = stateMachine.blackBoard.GetValue<Vector3>(pointName);
        if (destination != null)
        {
            NavMesh.SamplePosition(destination, out NavMeshHit hit, 5, NavMesh.AllAreas);
            stateMachine.agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogError("Point is undefined or inaccesible");
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.agent.velocity = Vector3.zero;
        stateMachine.agent.SetDestination(stateMachine.transform.position);
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        GetPoint(stateMachine);
    }

    public override void PhysicUpdate()
    {
    }
}
