using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] MouseLook mouseLook;
    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += x => playerMovement.OnJumpPressed();
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        playerMovement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        groundMovement.Enable();
    }

    private void OnDisable()
    {
        groundMovement.Disable();
    }
}
