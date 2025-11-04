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
            float distance = Vector3.Distance(stateMachine.transform.position, point.position);
            Debug.Log("Position " + stateMachine.transform.position + " target pos " + point.position);
            Debug.Log(distance);
            return distance < minDistance;
        }
        return false;
    }
}