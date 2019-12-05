using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public WheelCollider wheelCollider;

    private Vector3 wheelPos;
    private Quaternion wheelRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wheelCollider.GetWorldPose(out wheelPos, out wheelRot);
        transform.position = wheelPos;
        transform.rotation = wheelRot;

        //wheelRot.z = 90;
    }

    private void FixedUpdate()
    {
    }
}
