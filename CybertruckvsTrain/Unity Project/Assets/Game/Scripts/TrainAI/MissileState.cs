using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileState : FSMState
{
    //Variable
    Health health;
    TrainController trainController;
    TrainShoot trainShoot;

    public MissileState(TrainController train)
    {
        stateID = FSMStateID.Missile;
        health = train.GetComponent<Health>();
        trainController = train;

        health = trainController.GetComponent<Health>();
        trainShoot = trainController.GetComponent<TrainShoot>();

    }

    public override void EnterStateInit()
    {

    }

    public override void Reason(Transform player, Transform npc)
    {
        if (IsInCurrentRange(npc, player.position, EnemyVehicleController.SHOOT_DIST))
        {
            //shoot.ShootTarget(player.position);
        }
        else
        {
            //shoot.StopShooting();
        }
        if (health.HealthPercentage < 0.33)
        {
            trainController.PerformTransition(Transition.LowHP);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        //shoot missile guns only
    }
}
