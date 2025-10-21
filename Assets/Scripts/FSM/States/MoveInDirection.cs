
using UnityEngine;

[CreateAssetMenu(fileName ="MoveInDirection", menuName = "FSM/States/MoveInDirection")]
public class MoveInDirection : State
{
    public float speed = 2.0f;
    public Vector3 moveDirection;

    public override void Enter(StateMachine stateMachine)
    {
    }

    public override void Exit(StateMachine stateMachine)
    {
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(speed * Time.deltaTime * moveDirection.normalized, Space.Self);
    }

    public override void PhysicUpdate()
    {
    }
}