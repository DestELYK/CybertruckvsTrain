//  Name: ThirdPersonCharacterController.cs
//  Author: Connor Larsen
//  Purpose:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (!PauseMenu.gamePaused)
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3(0.0f, 0.0f, ver) * moveSpeed * Time.deltaTime;
            Vector3 playerRotation = new Vector3(0.0f, hor, 0.0f) * turnSpeed * Time.deltaTime;

            transform.Translate(playerMovement, Space.Self);
            transform.Rotate(playerRotation, Space.Self);
        }
    }
}