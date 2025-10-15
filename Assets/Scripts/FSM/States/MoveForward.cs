
using UnityEngine;

[CreateAssetMenu(fileName ="MoveForwardState", menuName = "FSM/States/MoveForward")]
public class MoveForward : State
{
    public float speed = 2.0f;


    public override void Enter(StateMachine stateMachine)
    {
    }

    public override void Exit(StateMachine stateMachine)
    {
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(speed * Time.deltaTime * Vector3.forward,Space.Self);
    }

    public override void PhysicUpdate()
    {
    }
}