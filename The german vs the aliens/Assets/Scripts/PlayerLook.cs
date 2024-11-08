using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    public float cameraDistance = 0.5f;

    void Start()
    {
        xRotation = cam.transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector3 direction = cam.transform.localPosition.normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, cameraDistance))
        {
            cam.transform.localPosition = direction * (hit.distance - 0.1f);
        }
        else
        {
            cam.transform.localPosition = direction * cameraDistance;
        }
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
