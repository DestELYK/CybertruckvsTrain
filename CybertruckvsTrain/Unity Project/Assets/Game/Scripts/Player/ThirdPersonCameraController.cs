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
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            //mouseY = Mathf.Clamp(mouseY, -35, 60);

            //transform.LookAt(target);

            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

            turret.rotation = rotation;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}