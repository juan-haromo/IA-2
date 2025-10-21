using UnityEngine;

[CreateAssetMenu(fileName = "CheckDistance", menuName = "FSM/Conditions/CheckDistance")]
public class CheckDistance : Condition
{
    [SerializeField] float minDistance;
    [SerializeField] string pointName;
    public override bool Check(StateMachine stateMachine)
    {
        Vector3 point = stateMachine.blackBoard.GetValue<Vector3>(pointName);

        if(point != null)
        {
            return Vector3.Distance(stateMachine.transform.position, point) < minDistance;
        }
        return false;
    }
}