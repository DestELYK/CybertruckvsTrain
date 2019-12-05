/* Name: Kyle Dunn
 * 
 * 
 */

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class EnemyVehicleController : AdvancedFSM
{
    public static int SLOT_DIST = 4;
    public static int ATTACK_DIST = 50;
    public static int SHOOT_DIST = 30;

    private bool debugDraw;

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    // Initialize the FSM for the NPC tank.
    protected override void Initialize()
    {
        debugDraw = true;

        // Find the Player and init appropriate data.
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        //Find waypoints
        //pointList = GameObject.FindGameObjectsWithTag("WayPoint");

        // Create the FSM for the tank.
        ConstructFSM();

    }

    /// <summary>
    /// Kyle Dunn
    /// Adds all states to the FSM
    /// </summary>
    private void ConstructFSM()
    {
        // Create States and add transitions to them
        #region State Creation/Transitions
        PatrolState patrol = new PatrolState(this);
        patrol.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Attacking);

        AttackState attack = new AttackState(this);
        attack.AddTransition(Transition.Enable, FSMStateID.Patrolling);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        DeadState dead = new DeadState();
        #endregion

        // Add the states to the FSM
        AddFSMState(patrol);
        AddFSMState(attack);
        AddFSMState(dead);
    }

    // Update each frame.
    protected override void FSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Reason(playerTransform, transform);
            CurrentState.Act(playerTransform, transform);
        }
    }
    protected override void FSMFixedUpdate()
    {
    }

    private void OnEnable()
    {
        if (CurrentState != null)
            PerformTransition(Transition.Enable);
    }
}
