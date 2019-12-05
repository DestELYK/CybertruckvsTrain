using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMState
{
    EnemyVehicleController enemyController;
    EnemyMovement movement;
    Health health;

    SlotManager slotManager;

    Vector3 destination;

    public PatrolState(EnemyVehicleController controller)
    {
        stateID = FSMStateID.Patrolling;
        enemyController = controller;
        movement = enemyController.GetComponent<EnemyMovement>();
        health = enemyController.GetComponent<Health>();
        slotManager = enemyController.GetPlayerTransform().GetComponent<SlotManager>();
    }

    public override void EnterStateInit()
    {
        destination = movement.FindRandomDestination();
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (health && health.HealthPercentage <= 0)
        {
            enemyController.PerformTransition(Transition.NoHealth);
        }
        else if (!slotManager.Filled && IsInCurrentRange(npc, player.position, EnemyVehicleController.ATTACK_DIST))
        {
            enemyController.PerformTransition(Transition.SawPlayer);
        }
        else if (movement.PathReached)
        {
            destination = movement.FindRandomDestination();
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        movement.SetDestination(destination);
    }
}
