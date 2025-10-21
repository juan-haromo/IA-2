
using UnityEngine;
[CreateAssetMenu(fileName = "WaitForDelay", menuName ="FSM/Conditions/WaitForDelay")]
public class WaitForDelay : Condition
{
    [SerializeField] string delayName;
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.blackBoard.GetValue<float>(delayName) < Time.time;
        
    }
}