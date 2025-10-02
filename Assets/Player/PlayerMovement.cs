using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    Vector2 horizontalInput;
    [SerializeField] float gravity = -9.81f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    [SerializeField] float jumpHeight = 3.5f;
    bool isJumping;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0f;
        }
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
        if (isJumping)
        {
            if (isGrounded)
            {
                verticalVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            isJumping = false;
        }
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
        //print(horizontalInput);
    }

    public void OnJumpPressed()
    {
        isJumping = true;
    }
}
