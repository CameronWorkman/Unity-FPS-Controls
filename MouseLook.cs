using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform playerBody;

    float xRotation = 0f;

    private void Update()
    {
        // sets mouseX and mouseY to raw mouse controller movements. multiples by sensitivity to allow control.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //rotates camera in the Y axis based on mouse movements, clamped to prevent player from looking past 90 degrees above or below themselves.
        //slippy was like "do a barrel roll" and I said NO I WILL NOT.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotates the parent transform object in the X axis based on mouse movements. allows control of player object based on directional view.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}