using UnityEngine;

[CreateAssetMenu(fileName = "CheckFarTransform", menuName = "FSM/Conditions/CheckFarTransform")]
public class CheckFarTransform : Condition
{
    [SerializeField] string transformKey;
    [SerializeField] string centerKey;
    [SerializeField] float maxDistance;

    public override bool Check(StateMachine stateMachine)
    {
        Transform point = stateMachine.blackBoard.GetValue<Transform>(transformKey);
        if (transformKey == default || transformKey == null) { return true; }
        Transform center = stateMachine.blackBoard.GetValue<Transform>(centerKey);
        if (center == default || center == null) { return true; }

        return  maxDistance < Vector3.Distance(point.position, center.position) ;
    }
}
