
using UnityEngine;

[CreateAssetMenu(fileName = "AlwaysTrue", menuName = "FSM/Conditions/AlwaysTrue")]
public class AlwaysTrue : Condition
{
    public override bool Check(StateMachine stateMachine)
    {
        return true;
    }
}