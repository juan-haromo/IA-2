using UnityEngine;

[CreateAssetMenu(fileName = "CheckTransformDistance", menuName = "FSM/Conditions/CheckTransformDistance")]
public class CheckTransformDistance : Condition
{
    [SerializeField] float minDistance;
    [SerializeField] string transformName;
    public override bool Check(StateMachine stateMachine)
    {
        Transform point = stateMachine.blackBoard.GetValue<Transform>(transformName);

        if(point != null)
        {
            return Vector3.Distance(stateMachine.transform.position, point.position) < minDistance;
        }
        return false;
    }
}