using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    LvlManager lvlManager;

    //--------------------SUBKLASSER--------------------------------

    public BaseState currentState;
    public EnteringState enteringState = new();
    public WalkToTableState walkToTableState = new();
    public SittingDownState sittingState = new();
    public OrderState orderState = new();
    public WaitingState waitingState = new();
    public EatingState eatingState = new();
    public StandingUpState standingUpState = new();
    public LeavingState leavingState = new();


    //-------------------------------------------------------------
    public Transform[] seats;
    public Transform chosenSeat;
    public NavMeshAgent navMeshAgent;
    public Canvas canvas;


    public GameObject timerBarPrefab;
    public GameObject timerBarInstance;
    public TimerBar timerBar;

    private string[] seatNames = { "Chair1", "Chair2", "Chair3", "Chair4", "Chair5", "Chair6", "Chair7", "Chair8" };
    public SeatManager[] seatManagers;

    public GameObject leavingStorePosition;
    public GameObject enterStorePosition;


    public int int_chosenTable;
    public bool isAnnoyed = false;
    public bool isAngry = false;
    public float timeLeftOnOrder = 0;
    public int waitingForOrderTime = 60;
    public int waitingForFoodTime = 120;
    public int eatingTime = 60;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool walking;
    public Vector3 popupPosition;
    public Color lightBlue;
    public Color purple;

    void Start()
    {
        lvlManager = FindAnyObjectByType<LvlManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;


        canvas = GameObject.FindWithTag("WorldCanvas").GetComponent<Canvas>();



        leavingStorePosition = GameObject.Find("CustomerLeavingPosition");
        enterStorePosition = GameObject.Find("EnteredStorePosition");

        seats = new Transform[seatNames.Length];
        seatManagers = new SeatManager[seats.Length];


        for (int i = 0; i < seatNames.Length; i++)
        {
            GameObject patrolPointObj = GameObject.Find(seatNames[i]);
            seats[i] = patrolPointObj.transform;
            seatManagers[i] = seats[i].GetComponent<SeatManager>();

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
        return (timeLeftOnOrder / (float) waitingForFoodTime);
    }

    public void DestroyCustomer()
    {
        // conceting to lvl manager Arvid
        if (lvlManager != null)
        {
            lvlManager.GetGold(GetTimeLeftOnOrder());
            lvlManager.AgentDestroyed();
        }
        Destroy(gameObject);
    }

    public void DestroyTimeBar()
    {
        Destroy(timerBarInstance);
    }

    public void AnimationDirection()
    {
        //Flippar gubben åt rätt håll när han går någonstans.
        if (transform.position.x < navMeshAgent.destination.x && walking == true)
        {
            //Debug.Log("going right");

            spriteRenderer.flipX = false;
        }
        if (transform.position.x > navMeshAgent.destination.x && walking == true)
        {
            //Debug.Log("going left");

            spriteRenderer.flipX = true;
        }
    }

    public void SittingDirection()
    {
        //Flippar gubben åt rätt håll när han sätter sig.
        if (chosenSeat.GetComponent<SpriteRenderer>().flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void SpawnTimerBar()
    {
        //Debug.Log("spawned bar");
        timerBarInstance = Instantiate(timerBarPrefab, transform.position, Quaternion.identity, canvas.transform);

        timerBar = timerBarInstance.GetComponent<TimerBar>();
    }

}
