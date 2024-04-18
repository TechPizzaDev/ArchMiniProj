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

    public GameObject agent;
    public Transform[] tables;
    //public string[] tableNames = {"Table1", "Table2", "Table3", "Table4"};
    public string[] tableNames = { "Chair1", "Chair2", "Chair3", "Chair4" };
    public TableManager[] tableManager;
    public GameObject leavingStorePosition;

    public int int_chosenTable;
    public bool isAnnoyed = false;
    public bool isAngry = false;
    public float timeLeftOnOrder;


    void Start()
    {
        leavingStorePosition = GameObject.Find("CustomerLeavingPosition");
        tables= new Transform[tableNames.Length];
        tableManager = new TableManager[tables.Length];

        for (int i = 0; i < tableNames.Length; i++)
        {
            GameObject patrolPointObj = GameObject.Find(tableNames[i]);
            tables[i] = patrolPointObj.transform;
            tableManager[i] = tables[i].GetComponent<TableManager>();
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
