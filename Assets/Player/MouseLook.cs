using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;
    [SerializeField] Transform playerCamera;
    [SerializeField] float XClamp = 85f;
    float xRotation = 0f;

    private void Update()
    {
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -XClamp, XClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReceiveInput(Vector2 _mouseInput)
    {
        mouseX = _mouseInput.x * sensitivityX;
        mouseY = _mouseInput.y * sensitivityY;
    }
}
