
using UnityEngine;

[CreateAssetMenu(fileName = "InDanger", menuName = "FSM/Conditions/InDanger")]
public class InDanger : Condition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.blackBoard.GetValue<bool>("InDanger");
    }
}