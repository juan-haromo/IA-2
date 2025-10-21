using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "BuffSpeed",menuName = "FSM/States/BuffSpeed")]
public class BuffSpeed : State
{
    [SerializeField] float speedAmount;
    [SerializeField] float duration;
    public override void Enter(StateMachine stateMachine)
    {
        NavMeshAgent allyAgent = stateMachine.blackBoard.GetValue<StateMachine>("Ally").agent;
        if (allyAgent != null || allyAgent == default)
        {
            stateMachine.StartCoroutine(SpeedBuff(speedAmount, duration, allyAgent));
        }
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

    IEnumerator SpeedBuff(float speedAmount, float duration, NavMeshAgent agent)
    {
        agent.speed += speedAmount;
        yield return new WaitForSeconds(duration);
        agent.speed -= speedAmount;
    }
}