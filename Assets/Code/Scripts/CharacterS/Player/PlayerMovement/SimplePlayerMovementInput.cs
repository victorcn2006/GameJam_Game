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
    [SerializeField] Image exclamationImage2;
    [Header("Parry")]
    [SerializeField] InputActionReference attackAction;

    [SerializeField] Transform boss;

    bool canParry;
    GameObject currentLaser;

    [Header("Colliders")]
    [SerializeField] Collider interactionCollider;
    [SerializeField] Collider parryCollider;

    [Header("Input Actions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference sprintAction;
    [SerializeField] InputActionReference interactionAction;

    CharacterController controller;
    Animator animator;

    Vector3 currentVelocity;

    bool canInteract;
    IInteractive _lastInteractiveObject;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        bool isSprinting = sprintAction.action.IsPressed();

        // Detectar si se está moviendo
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        // Animator
        animator.SetBool("Run", isMoving);

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
            if (canInteract) _lastInteractiveObject.Interact();

        exclamationImage.enabled = canInteract;

        if (attackAction.action.WasPressedThisFrame())
        {
            if (canParry && currentLaser != null)
            {
                ParryLaser(currentLaser);
            }
        }

        exclamationImage2.enabled = canParry;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;
            _lastInteractiveObject = other.GetComponent<IInteractive>();
        }

        if (other.CompareTag("Laser"))
        {
            canParry = true;
            currentLaser = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
            canInteract = false;

        if (other.CompareTag("Laser"))
        {
            canParry = false;
            currentLaser = null;
        }
    }

    void ParryLaser(GameObject laser)
    {
        if (boss == null || laser == null)
            return;

        Vector3 direction = (boss.position - laser.transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        laser.transform.rotation = rotation;

        Debug.Log("PARRY PERFECTO!");
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