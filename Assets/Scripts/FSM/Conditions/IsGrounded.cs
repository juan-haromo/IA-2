
using UnityEngine;

[CreateAssetMenu(fileName = "IsGrounded", menuName ="FSM/Conditions/IsGrounded")]
public class IsGrounded : Condition
{

    public float checkDistance;
    public LayerMask groundMask;
    public override bool Check(StateMachine stateMachine)
    {
        return Physics.Raycast(stateMachine.transform.position, Vector3.down, checkDistance, groundMask);
    }
}