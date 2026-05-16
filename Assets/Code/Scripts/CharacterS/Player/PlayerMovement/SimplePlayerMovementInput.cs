using Unity.Cinemachine;
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

    [Header("Shield")]
    [SerializeField] public bool hasShield;
    [SerializeField] GameObject shieldReference;
    [SerializeField] public GameObject shieldGetReference;

    [Header("UI References")]
    [SerializeField] Image exclamationImage;
    [SerializeField] Image exclamationImage2;
    [SerializeField] public CinemachineCamera obtainCamera;

    bool canParry;
    GameObject currentLaser;

    [Header("Colliders")]
    [SerializeField] Collider interactionCollider;

    [Header("Input Actions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference sprintAction;
    [SerializeField] InputActionReference interactionAction;
    [SerializeField] InputActionReference attackAction;

    CharacterController controller;
    public Animator animator;
    Transform laserOrigin;

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

        shieldReference.SetActive(hasShield);

        if (hasShield)
        {
            if (attackAction.action.WasPressedThisFrame())
            {
                if (canParry && currentLaser != null)
                {
                    ParryLaser(currentLaser);
                    animator.SetTrigger("Parry");
                }
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
        if (hasShield)
        {
            if (other.CompareTag("Laser"))
            {
                canParry = true;
                currentLaser = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
            canInteract = false;
        if (hasShield)
        {
            if (other.CompareTag("Laser"))
            {
                canParry = false;
                currentLaser = null;
            }
        }
    }

    void ParryLaser(GameObject laser)
    {
        laserOrigin = laser.GetComponent<LaserLightBehaviour>().origin;
        if (laserOrigin == null || laser == null)
            return;

        Vector3 direction = (laserOrigin.position - laser.transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        laser.transform.rotation = rotation;

        Debug.Log("PARRY PERFECTO!");
        laserOrigin = null;
    }

    public void EnableInput()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
        interactionAction.action.Enable();
    }

    public void DisableInput()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
        interactionAction.action.Disable();
    }

    void OnEnable()
    {
        EnableInput();
    }

    void OnDisable()
    {
        DisableInput();
    }
}