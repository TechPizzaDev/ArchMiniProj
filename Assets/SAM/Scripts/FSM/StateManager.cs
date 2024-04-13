using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{

    BaseState currentState;
    EnteringState enteringState = new EnteringState();
    WalkToTableState walkToTableState = new WalkToTableState();
    SittingDownState sittingState = new SittingDownState();
    OrderState orderState = new OrderState();
    WaitingState waitingState = new WaitingState();
    AnnoyedState annoyedState = new AnnoyedState();
    AngryState angryState = new AngryState();
    AngryLeavingState angryLeavingState = new AngryLeavingState();
    EatingState eatingState = new EatingState();
    StandingUpState standingUpState = new StandingUpState();
    LeavingState leavingState = new LeavingState();



    void Start()
    {
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

    
}
