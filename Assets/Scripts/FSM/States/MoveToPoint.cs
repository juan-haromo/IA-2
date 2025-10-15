
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPoint",menuName = "FSM/States/MoveToPoint")]
public class MoveToPoint : State
{
    public string pointName;
    public float stopDistance;
    public override void Enter(StateMachine stateMachine)
    {
        if (stateMachine.ContainsPoint(pointName, out TransformContext context))
        {
            stateMachine.agent.SetDestination(context.value.position);
        }
        else
        {
            Debug.LogError("Point " + pointName + " is undefined or inaccesible");
        } 
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.agent.velocity = Vector3.zero;
        stateMachine.agent.SetDestination(stateMachine.transform.position);
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {

    }

    public override void PhysicUpdate()
    {
    }
}
