using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlipVehicle : MonoBehaviour
{
    public const int TIME_BEFORE_FLIP = 5;

    public int rotateSpeed = 10;

    private float currentTime;

    private Rigidbody rigidbody;

    [SerializeField]
    private bool upsideDown;

    private bool flip;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.rotation * (Vector3.down * 10), Color.magenta);

        int layerMask = 1 << 9;

        if (!Physics.Raycast(transform.position, transform.rotation * (Vector3.down), 50, layerMask))
        {
            upsideDown = true;
        }
        else
        {
            upsideDown = false;
        }

        if (rigidbody.rotation == Quaternion.Euler(0, rigidbody.rotation.y, 0))
        {
            flip = false;
        }
    }

    private void FixedUpdate()
    {
        if (upsideDown)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= TIME_BEFORE_FLIP)
            {
                currentTime = 0;
                flip = true;
                rigidbody.velocity = Vector3.zero;
                transform.position += Vector3.up * 1.5f;
                rigidbody.AddForce(Vector3.up * 50);
            }
        }

        if (flip)
        {
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, Quaternion.Euler(0, rigidbody.rotation.y, 0), rotateSpeed * Time.deltaTime);

            if (rigidbody.rotation.x == 0)
            {
                flip = false;
                rigidbody.velocity = Vector3.zero;
            }
        }
    }
}
