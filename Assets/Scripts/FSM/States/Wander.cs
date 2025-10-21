using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Wander", menuName ="FSM/States/Wander")]
public class Wander : State
{
    [SerializeField] float wanderRadius;
    [SerializeField] float pointStopDistance;

    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] string wanderPointKey;


    public override void Enter(StateMachine stateMachine)
    {
        Vector3 movePoint = FindPoint(stateMachine.transform.position);
        stateMachine.blackBoard.SetValue<Vector3>(wanderPointKey, movePoint);
        stateMachine.agent.SetDestination(movePoint);
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

    Vector3 FindPoint(Vector3 startPos)
    {
        Vector3 randomPoint = startPos + (Random.insideUnitSphere * wanderRadius);

        NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas);
        return hit.position;
    }
}
