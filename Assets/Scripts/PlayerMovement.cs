using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    public float speed; 
    PlayerInput input;
    Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();
    }

    private void OnDestroy()
    {
        input.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = input.Player.Movement.ReadValue<Vector2>().x;
        movement.z = input.Player.Movement.ReadValue<Vector2>().y;

        characterController.Move(Time.deltaTime * speed * movement);
    }
}
