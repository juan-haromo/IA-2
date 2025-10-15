using UnityEngine;

[CreateAssetMenu(fileName = "AlterFloat",menuName = "FSM/States/AlterFloat")]
public class AlterFloat : State
{
    [SerializeField] float amount;
    [SerializeField] float inRate;
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] string key;

    const string TIME = "_time";

    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.AddNumber(key, stateMachine.ContainsNumber(key, out FloatContext context)? context.value : 0);
        stateMachine.AddNumber(key + TIME, Time.time);
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
        if (stateMachine.ContainsNumber(key, out FloatContext valueContext) && stateMachine.ContainsNumber(key + TIME, out FloatContext timeContext))
        {
            if (timeContext.value < Time.time)
            {
                stateMachine.UpdateNumber(key, Mathf.Clamp(valueContext.value + amount,minValue,maxValue));
                stateMachine.UpdateNumber(key + TIME, Time.time + inRate);
            }
        }
    }

    public override void PhysicUpdate()
    {
    }

    public override void Exit(StateMachine stateMachine)
    {
    }
}
