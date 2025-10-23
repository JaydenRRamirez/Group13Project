using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // small downward force to stay grounded

        // Get input
        float x = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Camera-relative movement
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f; // flatten
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = right * x + forward * z;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);

        // Smooth rotation to face movement direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
