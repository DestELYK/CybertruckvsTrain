using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleMovement : MonoBehaviour
{
    public Transform centerOfMass;

    public List<WheelCollider> thrustWheels;
    public List<WheelCollider> steeringWheels;

    public float maxSpeed = 20;
    public float turnSpeed = 30;
    public float acceleration = 10;

    float thrust;
    float steer;

    float currentSpeed;

    public float Thrust { set { thrust = value; } }

    public float Steer { set { steer = value; } get { return steer; } }

    public float CurrentSpeed { get { return currentSpeed; } }

    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = maxSpeed;

        DrawDebug();
    }

    private void FixedUpdate()
    {
        foreach (WheelCollider wheel in thrustWheels)
        {
            wheel.motorTorque = Mathf.Lerp(wheel.motorTorque, thrust * currentSpeed * 10, acceleration * Time.deltaTime);
        }

        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steer, turnSpeed * Time.deltaTime);
            //wheel.motorTorque = Mathf.Lerp(wheel.motorTorque, thrust * currentSpeed * 5, acceleration * Time.deltaTime);
        }
    }

    private void DrawDebug()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
        Debug.DrawRay(transform.position, transform.rotation * Quaternion.Euler(0, steeringWheels[0].steerAngle, 0) * Vector3.forward * 5, Color.red);
    }
}
