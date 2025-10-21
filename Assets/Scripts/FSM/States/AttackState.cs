using UnityEngine;


[CreateAssetMenu(fileName = "AttackState", menuName = "FSM/States/AttackState")]
public class AttackState : State
{
    [SerializeField] float attackRadius;
    [SerializeField] float damage;

    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.agent.SetDestination(stateMachine.transform.position);
        stateMachine.agent.velocity = Vector3.zero;
        Collider[] damageables = Physics.OverlapSphere(stateMachine.transform.position, attackRadius);
        foreach (Collider col in damageables)
        {
            if (col.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage(stateMachine.gameObject, damage);
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