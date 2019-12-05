//  Name: EnemyCounter.cs
//  Author: Connor Larsen
//  Purpose: Keeps track of enemy health and destroys enemy when dead

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text counterUI;  // Drag the UI text element for the counter here
    public bool allEnemiesDestroyed;   // True when all enemies are defeated

    private int numEnemies;  // Keeps track of the number of enemies in the world

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        numEnemies = 0;
        allEnemiesDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame, grab all the enemies in the scene, and set the total enemies left in the numEnemies int
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemies = enemies.Length;

        if (enemies.Length == 0)
        {
            allEnemiesDestroyed = true;
        }

        counterUI.text = "Enemies: " + numEnemies;  // Update the UI
    }
}