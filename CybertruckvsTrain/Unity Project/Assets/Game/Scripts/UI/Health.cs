//  Name: EnemyHealth.cs
//  Author: Connor Larsen
//  Purpose: Keeps track of both player and enemy health and destroys object when dead

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public Image healthBarImage;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarImage.fillAmount = HealthPercentage;

        if (currentHealth <= 0)
        {
            // Destroy enemy
            Destroy(gameObject);
        }
    }

    public float HealthPercentage
    {
        get
        {
            return currentHealth / startingHealth;
        }
    }
}