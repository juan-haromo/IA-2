using UnityEngine;

[CreateAssetMenu(fileName = "MoveInPoints",menuName = "FSM/States/MoveInPoints")]
public class MoveInPoints : State
{
    public float pointStopDistance;
    public string RouteName;

    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.AddNumber(RouteName, 0);
    }

    public override void Exit(StateMachine stateMachine)
    {
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        if(stateMachine.TryGetComponent<Route>(out Route route)){
            if (Vector3.Distance(stateMachine.transform.position, route.points[route.currentPoint].position) < pointStopDistance)
            {
                route.currentPoint++;
                if (route.currentPoint >= route.points.Count)
                {
                    stateMachine.UpdateNumber(RouteName, 1);
                    route.currentPoint = 0;
                    route.completed = true;
                }
            }
            stateMachine.agent.SetDestination(route.points[route.currentPoint].position);
        }
    }

    public override void PhysicUpdate()
    {
    }
}
