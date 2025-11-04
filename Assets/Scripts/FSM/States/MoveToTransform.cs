using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTransform",menuName ="FSM/States/MoveToTransform")]
public class MoveToTransform : State
{
    [SerializeField] string transformName;
    [SerializeField] float stopDistance;
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
        if (destination == null || destination == default) { return; }
        if (Vector3.Distance(stateMachine.transform.position,destination.position) <= stopDistance) { stateMachine.agent.SetDestination(stateMachine.transform.position); }
        else{stateMachine.agent.SetDestination(destination.position);}
        
    }

    public override void PhysicUpdate()
    {

    }
}
