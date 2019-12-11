using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleMovement))]
public class PlayerController : MonoBehaviour
{
    public Transform com;

    public float maxAngle = 45;

    VehicleMovement movement;

    public bool ableToMove = true;

    private void Awake()
    {
        ableToMove = true;

        GetComponent<Rigidbody>().centerOfMass = com.localPosition;
        movement = GetComponent<VehicleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            movement.enabled = true;

            movement.Thrust = Input.GetAxis("Vertical");
            movement.Steer = Input.GetAxis("Horizontal") * maxAngle;
        }
        else
        {
            movement.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Train")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.AddExplosionForce(1000, collision.GetContact(0).point, 10);
        }
    }
}
