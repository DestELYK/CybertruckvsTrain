using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : FSMState
{
    //Variable
    Health health;  
    TrainController trainController;
    EnemyShoot shoot;

    float elapsedTime;
    float intervalTime;

    public BulletState(TrainController train)
    {
        stateID = FSMStateID.Bullet;
        health = train.GetComponent<Health>();
        trainController = train;

        health = trainController.GetComponent<Health>();
        shoot = trainController.GetComponent<EnemyShoot>();

    }

    public override void EnterStateInit()
    {

    }

    public override void Reason(Transform player, Transform npc)
    {
        if (IsInCurrentRange(npc, player.position, EnemyVehicleController.SHOOT_DIST))
        {
            shoot.ShootTarget(player.position);
        }
        else
        {
            shoot.StopShooting();
        }
        if(health.HealthPercentage < 0.66)
        {
            trainController.PerformTransition(Transition.MedHP);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        //shoot chain guns only
    }
}
