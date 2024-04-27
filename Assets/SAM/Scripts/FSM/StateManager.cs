using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using System;
using static UnityEngine.Rendering.DebugUI;

public class StateManager : MonoBehaviour
{

    //--------------------SUBKLASSER--------------------------------

    public BaseState currentState;
    public EnteringState enteringState = new EnteringState();
    public WalkToTableState walkToTableState = new WalkToTableState();
    public SittingDownState sittingState = new SittingDownState();
    public OrderState orderState = new OrderState();
    public WaitingState waitingState = new WaitingState();
    public EatingState eatingState = new EatingState();
    public StandingUpState standingUpState = new StandingUpState();
    public LeavingState leavingState = new LeavingState();


    //-------------------------------------------------------------

    public Transform[] seats;
    public NavMeshAgent navMeshAgent;
    
    private string[] seatNames = { "Chair1", "Chair2", "Chair3", "Chair4", "Chair5", "Chair6", "Chair7", "Chair8" };
    public SeatManager[] seatManager;
    public GameObject leavingStorePosition;
    public GameObject enterStorePosition;


    public int int_chosenTable;
    public bool isAnnoyed = false;
    public bool isAngry = false;
    public float timeLeftOnOrder;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;


        leavingStorePosition = GameObject.Find("CustomerLeavingPosition");
        enterStorePosition = GameObject.Find("EnteredStorePosition");
        seats = new Transform[seatNames.Length];
        seatManager = new SeatManager[seats.Length];

        for (int i = 0; i < seatNames.Length; i++)
        {
            GameObject patrolPointObj = GameObject.Find(seatNames[i]);
            seats[i] = patrolPointObj.transform;
            seatManager[i] = seats[i].GetComponent<SeatManager>();
        }    

        currentState = enteringState;
        currentState.EnterState(this);
       

    }

    // Update is called once per frame
    void Update()
    {
       
        currentState.UpdateState(this);

    }

    

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public float GetTimeLeftOnOrder()
    {
        return timeLeftOnOrder;
    }
}
