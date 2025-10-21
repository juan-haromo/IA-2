using UnityEngine;

[CreateAssetMenu(fileName = "CheckForAllyState", menuName = "FSM/Conditions/CheckForAllyState")]
public class CheckForAllyState : Condition
{
    [SerializeField] string stateName;
    public override bool Check(StateMachine stateMachine)
    {
        StateMachine ally = stateMachine.blackBoard.GetValue<StateMachine>("Ally");
        if (ally == null) { return false; }
        return ally.CurrentState.name == stateName;
    }
}