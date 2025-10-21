using UnityEngine;

[CreateAssetMenu(fileName = "AllyInDanger", menuName = "FSM/Conditions/AllyInDanger")]

public class AllyInDanger : Condition
{
    public override bool Check(StateMachine stateMachine)
    {
        StateMachine ally = stateMachine.blackBoard.GetValue<StateMachine>("Ally");
        if (ally == null) { return false; }
        return ally.blackBoard.GetValue<bool>("InDanger");
    }
}