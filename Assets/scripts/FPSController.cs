using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 300f;
    public Transform cameraHolder;

    public Vector3 minBounds; // минимальные координаты комнаты
    public Vector3 maxBounds; // максимальные координаты комнаты

    private float xRotation = 0f;
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        LookAround();
        ClampPosition();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        pos.z = Mathf.Clamp(pos.z, minBounds.z, maxBounds.z);
        transform.position = pos;
    }
}
