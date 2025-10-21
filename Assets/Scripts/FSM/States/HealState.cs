using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName ="FSM/States/Heal")]
public class HealState : State
{
    [SerializeField] float healRadius;
    [SerializeField] float healAmount;
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.agent.SetDestination(stateMachine.transform.position);
        stateMachine.agent.velocity = Vector3.zero;
        Collider[] damageables = Physics.OverlapSphere(stateMachine.transform.position, healRadius);
        foreach (Collider col in damageables)
        {
            if (col.TryGetComponent<IHealable>(out IHealable healable))
            {
                healable.Heal(healAmount);
            }
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
    }

    public override void FrameUpdate(StateMachine stateMachine)
    {
    }

    public override void PhysicUpdate()
    {
    }
}
