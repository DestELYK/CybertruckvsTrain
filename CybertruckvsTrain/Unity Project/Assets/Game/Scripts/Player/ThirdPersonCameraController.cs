//  Name: ThirdPersonCameraController.cs
//  Author: Connor Larsen
//  Purpose:

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Transform target, player, turret;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseY = -90;
    }

    private void LateUpdate()
    {
        CamControl();
    }

    private void CamControl()
    {
        if (!PauseMenu.gamePaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            //mouseX = Mathf.Clamp(mouseX, -100, 100);
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -150, -79);

            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

            turret.localRotation = rotation;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}