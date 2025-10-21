using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTransform",menuName ="FSM/States/MoveToTransform")]
public class MoveToTransform : State
{
    [SerializeField] string transformName;
    public override void Enter(StateMachine stateMachine)
    {

    }

    public override void Exit(StateMachine stateMachine)
    {

    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        Transform destination = stateMachine.blackBoard.GetValue<Transform>(transformName);
        Debug.Log(destination.gameObject);
        if( destination == null || destination == default) { return; }
        stateMachine.agent.SetDestination(destination.position);
    }

    public override void PhysicUpdate()
    {

    }
}
