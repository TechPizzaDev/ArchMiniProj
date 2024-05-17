using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Canvas canvas;


    public GameObject timerBarPrefab;
    private GameObject timerBarInstance;
    private Slider timerBarSlider;

    private string[] seatNames = { "Chair1", "Chair2", "Chair3", "Chair4", "Chair5", "Chair6", "Chair7", "Chair8" };
    public SeatManager[] seatManager;
    //public SpriteRenderer[] seatSprite;
    public GameObject leavingStorePosition;
    public GameObject enterStorePosition;


    public int int_chosenTable;
    public bool isAnnoyed = false;
    public bool isAngry = false;
    public float timeLeftOnOrder;
    public int waitingTime = 120;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool walking;

    void Start()
    {


        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;



        //timerBarScript.SetMaxTime(waitingTime);


        canvas = GameObject.FindWithTag("WorldCanvas").GetComponent<Canvas>();
        SpawnTimerBar();


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

        //TimerBar

        timerBarInstance.transform.position = transform.position;





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
        Destroy(timerBarInstance);
    }

    public void AnimationDirection()
    {
        //Flippar gubben åt rätt håll när han går någonstans.
        if (transform.position.x < navMeshAgent.destination.x && walking == true)
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

    void SpawnTimerBar()
    {
        Debug.Log("spawned bar");
        timerBarInstance = Instantiate(timerBarPrefab, transform.position, Quaternion.identity, canvas.transform);
        timerBarSlider = timerBarInstance.GetComponent<Slider>();
    }

}
