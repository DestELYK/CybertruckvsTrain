using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    const float RESET_SLOT_INTERVAL = 2;

    EnemyVehicleController enemyController;
    EnemyMovement movement;
    Health health;
    EnemyShoot shoot;

    VehicleMovement playerMovement;

    SlotManager slotManager;

    Vector3 destination;

    int slotIndex;

    float resetSlotTime;


    public AttackState(EnemyVehicleController controller)
    {
        stateID = FSMStateID.Attacking;
        enemyController = controller;
        slotIndex = -1;

        movement = enemyController.GetComponent<EnemyMovement>();
        health = enemyController.GetComponent<Health>();
        shoot = enemyController.GetComponent<EnemyShoot>();
        playerMovement = enemyController.GetPlayerTransform().GetComponent<VehicleMovement>();
        slotManager = enemyController.GetPlayerTransform().GetComponent<SlotManager>();
    }

    public override void EnterStateInit()
    {
        resetSlotTime = 0;

        FindNewSlot();
    }

    public override void Reason(Transform player, Transform npc)
    {
        resetSlotTime += Time.deltaTime;
        if (resetSlotTime >= RESET_SLOT_INTERVAL)
        {
            resetSlotTime = 0;
            FindNewSlot();
        }

        if (IsInCurrentRange(npc, player.position, EnemyVehicleController.SHOOT_DIST))
        {
            Vector3 target = (player.position + player.GetComponent<Rigidbody>().velocity) + Vector3.down * 0.5f;

            shoot.ShootTarget(target);
        }
        else
        {
            shoot.StopShooting();
        }

        if (health && health.HealthPercentage <= 0)
        {
            enemyController.PerformTransition(Transition.NoHealth);
        }
        else
        {
            destination = slotManager.GetSlotPosition(slotIndex);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        movement.SetDestination(destination);
    }

    private void FindNewSlot()
    {
        slotManager.ReleaseSlot(slotIndex);
        slotIndex = slotManager.FillSlot(enemyController.gameObject);
    }
}
