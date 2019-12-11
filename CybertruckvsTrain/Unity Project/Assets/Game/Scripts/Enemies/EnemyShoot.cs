using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private const int SHOOT_INTERVAL = 3;

    [SerializeField]
    private GameObject missilePrefab;

    [SerializeField]
    private Transform spawn;

    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private int minAngle = 6;
    [SerializeField]
    private int maxAngle = -30;

    private Vector3 target;

    private bool shooting;

    private float shootTime;

    public void ShootTarget(Vector3 target)
    {
        this.target = target;

        if (!shooting)
        {
            shooting = true;
            shootTime = SHOOT_INTERVAL;
        }
    }

    public void StopShooting()
    {
        shooting = false;
    }

    private void Awake()
    {
        shootTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            shootTime += Time.deltaTime;
            if (shootTime >= SHOOT_INTERVAL)
            {
                shootTime = 0;
                CreateMissile();
            }
        }
    }

    private void FixedUpdate()
    {
        if (shooting)
        {
            Vector3 direction = target - pivot.position;

            Quaternion rotation = Quaternion.LookRotation(direction);

            pivot.rotation = rotation;
        }
        else
        {
            pivot.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void CreateMissile()
    {
        MissileTarget missile = Instantiate<GameObject>(missilePrefab, spawn.position, pivot.rotation).GetComponent<MissileTarget>();
        missile.Target = target;
        missile.VehicleSpeed = GetComponent<Rigidbody>().velocity.magnitude;
    }
}
