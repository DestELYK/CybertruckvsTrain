using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainShoot : MonoBehaviour
{
    [SerializeField]
    private EnemyShoot[] turrets;

    public float GetTurretHealth(int index)
    {
        return turrets[index].GetComponent<Health>().HealthPercentage;
    }

    public void ShootTarget(Vector3 position)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
