using UnityEngine;


[CreateAssetMenu(fileName = "ActivateInteraction",menuName = "FSM/States/ActivateInteraction")]
public class ActivateInteraction : State
{
    [SerializeField] float interactionRadius;
    public override void Enter(StateMachine stateMachine)
    {
        Collider[] interactables = Physics.OverlapSphere(stateMachine.transform.position, interactionRadius);
        foreach (Collider col in interactables)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(stateMachine.gameObject);
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