using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerMovementInput : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float walkSpeed = 8f;
    [SerializeField] float sprintSpeed = 12f;
    [SerializeField] float acceleration = 12f;
    [SerializeField] float rotationSpeed = 8f;


    [Header("UI References")]
    [SerializeField] Image exclamationImage;

    [Header("Colliders")]
    [SerializeField] Collider interactionCollider;

    [Header("Input Actions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference sprintAction;
    [SerializeField] InputActionReference interactionAction;

    CharacterController controller;

    Vector3 currentVelocity;

    bool canInteract;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        bool isSprinting = sprintAction.action.IsPressed();

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Dirección deseada
        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // Aceleración suave tipo steering
        Vector3 targetVelocity = inputDirection * targetSpeed;

        currentVelocity = Vector3.Lerp(
            currentVelocity,
            targetVelocity,
            acceleration * Time.deltaTime
        );

        // Movimiento
        controller.Move(currentVelocity * Time.deltaTime);

        // Rotación suave basada en la velocidad real
        Vector3 horizontalVelocity = new Vector3(
            currentVelocity.x,
            0f,
            currentVelocity.z
        );

        if (horizontalVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(horizontalVelocity);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (interactionAction.action.WasPressedThisFrame())
            if(canInteract) Debug.Log("Interactuar");

        exclamationImage.enabled = canInteract;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interactable"))
            canInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
            canInteract = false;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
        interactionAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
        interactionAction.action.Disable();
    }
}