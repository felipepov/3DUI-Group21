using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // movement speed

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from keyboard
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Create move direction relative to player's orientation
        Vector3 move = transform.right * moveZ + transform.forward * (-moveX);

        // Apply movement
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}