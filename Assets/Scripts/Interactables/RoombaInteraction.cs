using UnityEditor;
using UnityEngine;

public class RoombaInteraction : MonoBehaviour, IInteractable
{
    public StateMachine roomba;
    public void Interact(GameObject interactor)
    {
        roomba.UpdateNumber("cleaning", 1);
    }
}