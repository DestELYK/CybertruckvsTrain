//  Name: TurretShoot.cs
//  Author: Connor Larsen
//  Purpose: Using raycast, allows the turret on the player to shoot

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public float fireRate = 0.1f;   // The fire rate of the gun
    public int damage = 10;         // How much damage each shot does
    public Transform firePoint;     // Where the bullets shoot from

    private float timer;            // Used to set the fire rate of the gun

    #region Kyle Dunn

    public float animationRate = 0.1f;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Animator turretAnimator;

    #endregion

    // Update is called once per frame
    void Update()
    {  
        // Increment the timer until it hits the fireRate, then allows the turret to fire
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetMouseButton(0))
            {
                timer = 0f;
                FireTurret();
                audioSource.Play(); // Kyle Dunn
                turretAnimator.SetBool("Firing", true);
            }
        }

        if (timer >= animationRate)
        {
            turretAnimator.SetBool("Firing", false);
        }
    }

    private void FireTurret()
    {
        //Ray ray = new Ray(firePoint.position, firePoint.forward * 100);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<Health>();
            if (health != null && hitInfo.collider.gameObject.tag != "Player")
            {
                health.TakeDamage(damage);
            }
        }

        Debug.DrawRay(firePoint.position, Camera.main.transform.forward * 100, Color.red, fireRate);
    }
}