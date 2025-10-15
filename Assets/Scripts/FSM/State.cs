using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public List<Transition> transitions;

    public abstract void Enter(StateMachine stateMachine);
    public abstract void Exit(StateMachine stateMachine);
    public abstract void FrameUpdate(StateMachine stateMachine);
    public abstract void PhysicUpdate();

    public virtual void CheckTransitions(StateMachine stateMachine)
    {
        foreach (Transition transition in transitions)
        {
            if (transition.condition != null && transition.condition.Check(stateMachine))
            {
                stateMachine.ChangeState(transition.state);
                break;
            }
        }
    }
}
