using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMotor : MonoBehaviour {
    [SerializeField] private Camera cam;
    [SerializeField] private float runningCamFOV = 59.3f;
    [SerializeField] private float orginalFOV = 53.96585f;
    [SerializeField] private float dynamicFOVspeed = 5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool IsGrounded;
    private bool lerpCrouch;
    private bool crouching;
    private bool sprinting;
    private float crouchTimer;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    void Start() {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        IsGrounded = controller.isGrounded;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
            speed = 8;
            runningCamFOV = 104f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
            speed = 5;
            runningCamFOV = 85f;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, runningCamFOV, dynamicFOVspeed * Time.deltaTime);

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    //recives the inputs from our InputManager.cs and apply them to our character contoroller.
    public void ProcessMove(Vector2 input) {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump() {
        if (IsGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch() {
        crouching = !crouching;
        crouchTimer = 0;
        speed = 3;
        lerpCrouch = true;
        if (!crouching) {
            speed = 5;
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if (sprinting)
        {
            speed = 8;
            runningCamFOV = 104f;
        }
        else
        {
            speed = 5;
            runningCamFOV = 85f;
        }
    }

}