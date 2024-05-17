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
    public Transform chosenSeat;
    public NavMeshAgent navMeshAgent;

    private string[] seatNames = { "Chair1", "Chair2", "Chair3", "Chair4", "Chair5", "Chair6", "Chair7", "Chair8" };
    public SeatManager[] seatManager;
    //public SpriteRenderer[] seatSprite;
    public GameObject leavingStorePosition;
    public GameObject enterStorePosition;


    public int int_chosenTable;
    public bool isAnnoyed = false;
    public bool isAngry = false;
    public float timeLeftOnOrder;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool walking;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;




        leavingStorePosition = GameObject.Find("CustomerLeavingPosition");
        enterStorePosition = GameObject.Find("EnteredStorePosition");
        seats = new Transform[seatNames.Length];
        seatManager = new SeatManager[seats.Length];
        //seatSprite = new SpriteRenderer[seats.Length];

        for (int i = 0; i < seatNames.Length; i++)
        {
            GameObject patrolPointObj = GameObject.Find(seatNames[i]);
            seats[i] = patrolPointObj.transform;
            seatManager[i] = seats[i].GetComponent<SeatManager>();
            //seatSprite[i] = seats[i].GetComponent<SpriteRenderer>();
        }

        currentState = enteringState;
        currentState.EnterState(this);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Animator
        AnimationDirection();


        if (walking)
        {
            animator.SetBool("walking", true);
        }
        else if (!walking)
        {
            animator.SetBool("walking", false);
        }

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

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void AnimationDirection()
    {
        //Flippar gubben �t r�tt h�ll n�r han g�r n�gonstans.
        if (transform.position.x < navMeshAgent.destination.x && walking==true)
        {
            //Debug.Log("going right");
            //walking = true;
            spriteRenderer.flipX = false;
        }
        if (transform.position.x > navMeshAgent.destination.x && walking == true)
        {
            //Debug.Log("going left");
            //walking = true;
            spriteRenderer.flipX = true;
        }
    }

    public void SittingDirection()
    {
        //Flippar gubben �t r�tt h�ll n�r han s�tter sig.
        if (chosenSeat.GetComponent<SpriteRenderer>().flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
