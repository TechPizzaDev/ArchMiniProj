using UnityEngine;

public class LeavingState : BaseState
{
    float waitTime = 1;
    float timer = 0;
    bool leftTable = false;
    Transform navPosition;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered LeavingState...");
        timer = waitTime;
        navPosition = agent.leavingStorePosition.transform;
        agent.walking = true;
    }

    public override void UpdateState(StateManager agent)
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (leftTable == false)
            {
                agent.seatManagers[agent.int_chosenTable].FreeSeat();
                leftTable = true;
            }

            if (leftTable == true)
            {
                SetDestination(agent);
            }
        }

        if (Vector3.Distance(agent.navMeshAgent.nextPosition, navPosition.position) < 1f)
        {
            agent.DestroyCustomer();
        }
    }

    void SetDestination(StateManager agent)
    {
        if (!NavMeshHelper.Validate(agent.navMeshAgent)) return;

        agent.navMeshAgent.destination = navPosition.position;
    }
}
