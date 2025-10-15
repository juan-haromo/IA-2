
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName ="CloseGame", menuName = "FSM/States/CloseGame")]
public class CloseGame : State
{
    public override void Enter(StateMachine stateMachine)
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
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