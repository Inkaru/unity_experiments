using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;
    float xRotation = 0f;

    InputManager inputManager;
    Vector2 mouseDirection;

    void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Mouse.performed += ctx => mouseDirection = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseDirection.y * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseDirection.x * Time.deltaTime * mouseSensitivity);
    }

    void OnEnable()
    {
        inputManager.Enable();
    }

    void OnDisable()
    {
        inputManager.Disable();
    }
}
