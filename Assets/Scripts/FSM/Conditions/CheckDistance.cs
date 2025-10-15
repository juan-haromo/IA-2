using UnityEngine;

[CreateAssetMenu(fileName = "CheckDistance", menuName = "FSM/Conditions/CheckDistance")]
public class CheckDistance : Condition
{
    [SerializeField] float minDistance;
    [SerializeField] string pointName;
    public override bool Check(StateMachine stateMachine)
    {
        if(stateMachine.ContainsPoint(pointName, out TransformContext context))
        {
            return Vector3.Distance(stateMachine.transform.position, context.value.position) < minDistance;
        }
        return false;
    }
}