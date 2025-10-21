using UnityEngine;

[CreateAssetMenu(fileName = "Delay", menuName ="FSM/States/Delay")]
public class DelayState : State
{

    [SerializeField] string delayName;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.blackBoard.SetValue(delayName, Time.time + Random.Range(minDelay, maxDelay));
        stateMachine.agent.SetDestination(stateMachine.transform.position);
    }

    public override void Exit(StateMachine stateMachine)
    {
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
    }

    public override void PhysicUpdate()
    {
    }
}
