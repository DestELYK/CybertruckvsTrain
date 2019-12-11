//--------------------------------------------------------------------------------------------
// Title    : NPCTrainController
// Purpose  : This class is derived from AdvancedFSM and holds the FSM for the NPC Train
//            each train cart need this for behaviour.
// Author   : Patrick Werbner
// Date     : Dec 5, 2019
//---------------------------------------------------------------------------------------------

//todo: add transitions and states to AdvancedFSM


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TrainController : AdvancedFSM
{
    public static int MISSILE_DIST = 50; //missile can shoot player any where
    public static int CHAINGUN_DIST = 10; // close to medium range

    public Transform turret1;
    public Transform turret2;
    public Transform turret3;
    public Transform turret4;
    public Transform turret5;
    public Transform turret6;

    [HideInInspector]
    public Rigidbody rigBody;

    private Transform playerTransform;
    private bool debugDraw;

    public Transform GetPlayerTransform() //returns player positon for inrange checks
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

        rigBody = GetComponent<Rigidbody>();

        // Create the FSM for the train.
        ConstructFSM();

    }


    // Update each frame.
    protected override void FSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Reason(playerTransform, transform);
            CurrentState.Act(playerTransform, transform);
        }
        if (debugDraw)
        {
            // UsefullFunctions.DebugRay(transform.position, transform.forward * 5.0f, Color.red);
        }
    }
    protected override void FSMFixedUpdate()
    {
    }

    private void ConstructFSM()
    {
        BulletState bullet = new BulletState(this);
        bullet.AddTransition(Transition.MedHP, FSMStateID.Missile);

        MissileState missle = new MissileState(this);
        missle.AddTransition(Transition.LowHP, FSMStateID.Barrage);

        BarrageState barrage = new BarrageState(this);
        barrage.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        DeadState dead = new DeadState();

        //Add transition to FSM
        AddFSMState(bullet);
        AddFSMState(missle);
        AddFSMState(barrage);
        AddFSMState(dead);


    }

    private void OnEnable()
    {
        if (CurrentState != null)
            PerformTransition(Transition.Enable);
    }
}
