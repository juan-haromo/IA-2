using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1_Player : MonoBehaviour
{
    PlayerInput input;
    public Transform target;
    [SerializeField] SPA_Sheva sheva;

    #region Unity Calls
    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Shoot.started += Shoot;
        input.Player.SelfDamage.started += (InputAction.CallbackContext context) => Damage(10);
        input.Player.DamageSheva.started += (InputAction.CallbackContext context) => sheva.Damage(10);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentHealth = maxHealth;
    }


    void Update()
    {
        MoveCamera();
        MovePlayer();
        HandleGravity();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundDetectionRadius);
    }
    #endregion

    #region Camera
    [Header("Camera")]
    [SerializeField] Transform playerCamera;
    [SerializeField] float senseX;
    [SerializeField] float senseY;
    Vector2 mouseInput;
    float yRotation;
    float xRotation;
    void MoveCamera()
    {
        mouseInput = input.Player.CameraMovement.ReadValue<Vector2>();
        yRotation += mouseInput.x * Time.deltaTime * senseX;
        xRotation -= mouseInput.y * Time.deltaTime * senseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    #endregion

    #region Player Movement
    [Header("PlayerMovement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Transform orientation;
    CharacterController controller;
    Vector2 movementInput;
    Vector3 moveDirection;
    void MovePlayer()
    {
        movementInput = input.Player.Movement.ReadValue<Vector2>();
        moveDirection = (orientation.forward * movementInput.y) + (orientation.right * movementInput.x);
        controller.Move(Time.deltaTime * moveSpeed * moveDirection.normalized);
    }
    #endregion

    #region Gravity
    [Header("Gravity")]
    [SerializeField] LayerMask groundMask;

    [SerializeField] float groundDetectionRadius = 1;
    [SerializeField] Vector3 groundCheckOffset;
    void HandleGravity()
    {
        Collider[] groundCheck = Physics.OverlapSphere(transform.position + groundCheckOffset, groundDetectionRadius, groundMask);
        if (groundCheck.Length == 0)
        {
            controller.Move(Time.deltaTime * 9.81f * Vector3.down);
        }
    }
    #endregion

    #region  Health
    public float maxHealth;
    public float currentHealth { get; private set; }

    public void Damage(float amount)
    {
        currentHealth -= Mathf.Abs(amount);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    #endregion

    #region Shoot

    [SerializeField] ParticleSystem impactParticles;
    public int ammo = 50;
    void Shoot()
    {
        ammo--;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, 500))
        {
            impactParticles.gameObject.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            impactParticles.Play();
            if (hit.collider.CompareTag("Enemy")) { target = hit.collider.gameObject.transform; }
        }
    }
    private void Shoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Shoot();
    }
    #endregion
}
