using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    
    [Header("Sprint")]
    [SerializeField] private float sprintSpeed = 10.0f;
    private bool isSprinting = false;
    
    [Header("Crouch")]
    [SerializeField] private float crouchSpeed = 3.0f;
    [SerializeField] private float crouchHeight = 0.9f;
    private bool isCrouching = false;
    
    [Header("Jump")]
    [SerializeField] private float jumpPower = 5.0f;
    
    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 2000.0f;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraController cameraController;

    [Header("Player Property")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float height = 1.8f;

    // Components
    private CharacterController controller;

    // Input Actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction sprintAction;
    
    // Private attributes
    private float rotateX = 0.0f;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += context =>
        {
            if (controller.isGrounded)
            {
                velocity.y = jumpPower;
            }
        };

        crouchAction = InputSystem.actions.FindAction("Crouch");
        crouchAction.performed += context => { isCrouching = !isCrouching; };

        sprintAction = InputSystem.actions.FindAction("Sprint");
        sprintAction.started += context => { isSprinting = true; };
        sprintAction.canceled += context => { isSprinting = false; };
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        controller.height = isCrouching ? crouchHeight : height;
        cameraController.SetSprinting(isSprinting);
        crosshair.SetActive(!isSprinting);
        // Handle movement
        float currentSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : moveSpeed;
        // Set movement velocity in x and z axis.
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        velocity.x = moveValue.x * currentSpeed;
        velocity.z = moveValue.y * currentSpeed;
        // Handle gravity (dv_y = a_y * dt). Apply deceleration only in air.
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        // Update position (dx = v * dt).
        Vector3 motion = (
            transform.forward * velocity.z +  // Forward/Backward
            transform.right * velocity.x +  // Right/Left
            transform.up * velocity.y  // Jump
        ) * Time.deltaTime;
        controller.Move(motion);
        // Update camera.
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();
        // Rotate horizontally.
        float mouseX = lookValue.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        // Rotate vertically.
        float mouseY = lookValue.y * mouseSensitivity * Time.deltaTime;
        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -90.0f, 90.0f);
        cameraTransform.localRotation = Quaternion.Euler(rotateX, 0, 0);
    }
}
