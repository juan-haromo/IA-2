using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "WanderAroundTransform", menuName ="FSM/States/WanderAroundTransform")]
public class WanderAroundTransform : State
{
    [SerializeField] float wanderRadius;
    [SerializeField] string transformName;
    [SerializeField] string pointName;



    public override void Enter(StateMachine stateMachine)
    {
        Transform transformToWander = stateMachine.blackBoard.GetValue<Transform>(transformName);
        Vector3 movePoint = FindPoint(transformToWander.position);
        stateMachine.blackBoard.SetValue<Vector3>(pointName, movePoint);
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

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas);
        return hit.position;
    }
}