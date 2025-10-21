using UnityEngine;

[CreateAssetMenu(fileName = "CheckHealth", menuName = "FSM/Conditions/CheckHealth")]
public class CheckHealth : Condition
{
    public int minHealth;

    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.blackBoard.GetValue<int>("health") < minHealth;
    }
}