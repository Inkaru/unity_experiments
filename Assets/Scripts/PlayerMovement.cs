using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    InputManager inputManager;
    Vector2 movement;
    float jump;

    void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        inputManager.Player.Jump.performed += ctx => jump = ctx.ReadValue<float>();
        inputManager.Player.Jump.canceled += ctx => jump = ctx.ReadValue<float>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        controller.Move(move * speed * Time.deltaTime);

        if (jump > 0 && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
