using UnityEngine;

[CreateAssetMenu(fileName = "CheckFloat", menuName = "FSM/Conditions/CheckFloat")]
public class CheckForFloat : Condition
{
    public float minValue;
    public float maxValue;
    public bool checkForMin;
    public string valueName;

    public override bool Check(StateMachine stateMachine)
    {
        if (!stateMachine.ContainsNumber(valueName)) { return false; }
        if (checkForMin)
        {
            return stateMachine.GetNumber(valueName) <= minValue;
        }
        else
        {
            return maxValue <= stateMachine.GetNumber(valueName);
        }
    }
}