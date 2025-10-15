using UnityEngine;

[CreateAssetMenu(fileName = "RoombaMovement",menuName = "FSM/States/RoombaMovement")]
public class RoombaMovement : MoveInPoints
{
    [SerializeField] float batteryLoss;
    [SerializeField] float inRate;
    const string KEY = "battery";
    const string TIME = "_time";

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
    }
    public override void FrameUpdate(StateMachine stateMachine)
    {
        base.FrameUpdate(stateMachine);
        if (stateMachine.ContainsNumber(KEY, out FloatContext valueContext) && stateMachine.ContainsNumber(KEY + TIME, out FloatContext timeContext))
        {
            if (timeContext.value < Time.time)
            {
                stateMachine.UpdateNumber(KEY, Mathf.Clamp(valueContext.value - batteryLoss, 0, 100));
                stateMachine.UpdateNumber(KEY + TIME, Time.time + inRate);
            }
        }
    }
}