/* Name: Kyle Dunn
 * Date: Dec 4, 2019
 * Purpose: Moves missile towards given target when missile is created
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTarget : MonoBehaviour
{
    private const int MAX_TIME_ALIVE = 1;

    [SerializeField]
    private int speed = 20;

    [SerializeField]
    private int turnSpeed = 2;

    [SerializeField]
    private int damage = 10;

    private float vehicleSpeed;

    private float timeAlive;

    private Quaternion initialRotation;

    private Vector3 target;

    public Vector3 Target { set { target = value; initialRotation = transform.rotation; } }

    public float VehicleSpeed { set { vehicleSpeed = value; } }

    private void Update()
    {
        if (timeAlive < MAX_TIME_ALIVE)
            timeAlive += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timeAlive > MAX_TIME_ALIVE)
        {
            Quaternion rotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(90, initialRotation.y, 0), turnSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }

        transform.position += transform.forward * (speed + vehicleSpeed) * Time.deltaTime;

        //Quaternion rotation = Quaternion.LookRotation(target - transform.position);
        //rotation.y = transform.rotation.y;
        //rotation.z = 0;

        //Quaternion fallRotation = Quaternion.Euler(90, 0, 0);
        //fallRotation.y = transform.rotation.y;
        //fallRotation.z = 0;

        //rotation = Quaternion.Slerp(rotation, fallRotation, timeAlive / MAX_TIME_ALIVE);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        //transform.position += transform.forward * speed * Time.deltaTime;

        //Quaternion rotation = Quaternion.LookRotation(target - transform.position);

        //if (timeAlive >= MAX_TIME_ALIVE)
        //    rotation *= Quaternion.Euler(timeAlive, 0, 0);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO Effects
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2.0f);

        if (collision.gameObject.tag == "Player")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeDamage(damage);
        }
    }
}
