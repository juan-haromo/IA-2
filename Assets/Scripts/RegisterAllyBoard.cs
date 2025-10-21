using UnityEngine;

[DefaultExecutionOrder(-1)]
public class RegisterAlly : MonoBehaviour
{
    [SerializeField] StateMachine allyA;
    [SerializeField] StateMachine allyB;

    void Start()
    {
        allyA.blackBoard.SetValue<StateMachine>("Ally", allyB);
        allyA.blackBoard.SetValue<Transform>("AllyTransform", allyB.transform);
        allyB.blackBoard.SetValue<StateMachine>("Ally", allyA);
        allyB.blackBoard.SetValue<Transform>("AllyTransform", allyA.transform);
    }
}