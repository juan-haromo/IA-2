using UnityEngine;

[CreateAssetMenu(fileName = "CheckTime", menuName = "FSM/Conditions/CheckTime")]
public class CheckTime : Condition
{
    [SerializeField] float timeS;
    [SerializeField] bool trueBeforeSetTime;

    public override bool Check(StateMachine stateMachine)
    {
        if (trueBeforeSetTime)
        {
            return timeS > Clock.Instance.time;
        }
        else
        {
            return  timeS < Clock.Instance.time;
        }
    }
}