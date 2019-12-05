/* Name: Kyle Dunn
 * Date: Dec 4, 2019
 * Purpose: Moves missile towards given target when missile is created
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTarget : MonoBehaviour
{
    private const int MAX_TIME_ALIVE = 3;

    [SerializeField]
    private int speed;

    [SerializeField]
    private int turnSpeed;

    private float timeAlive;

    private Vector3 target;

    public Vector3 Target { set { target = value; } }

    private void Update()
    {
        if (timeAlive < MAX_TIME_ALIVE)
            timeAlive += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(target - transform.position);
        rotation.y = transform.rotation.y;
        rotation.z = 0;

        Quaternion fallRotation = Quaternion.Euler(90, 0, 0);
        fallRotation.y = transform.rotation.y;
        fallRotation.z = 0;

        rotation = Quaternion.Slerp(rotation, fallRotation, timeAlive / MAX_TIME_ALIVE);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;

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
        gameObject.SetActive(false);
        Destroy(gameObject, 5.0f);
    }
}
