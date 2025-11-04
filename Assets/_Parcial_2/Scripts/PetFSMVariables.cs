using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PetFSMVariables : MonoBehaviour
{
    [SerializeField] StateMachine stateMachine;

    [SerializeField] string playerKey;
    [SerializeField] Transform playerTransform;

    [SerializeField] string houseKey;
    [SerializeField] Transform houseTransform;

    [SerializeField] string houseRadiusKey;
    [SerializeField] float houseRadius;

    [SerializeField] string followRadiusKey;
    [SerializeField] float followRadius;


    void Awake()
    {
        stateMachine.blackBoard.SetValue<Transform>(playerKey, playerTransform);

        stateMachine.blackBoard.SetValue<Transform>(houseKey, houseTransform);

        stateMachine.blackBoard.SetValue<float>(houseRadiusKey, houseRadius);

        stateMachine.blackBoard.SetValue<float>(followRadiusKey, followRadius);
    }
}
