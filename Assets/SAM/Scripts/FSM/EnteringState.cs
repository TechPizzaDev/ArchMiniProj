using UnityEngine;

public class EnteringState : BaseState
{
    float waitTime = 2;
    float timer = 0;
    bool insideTheStore = false;

    Transform navPosition;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered EnteringState...");
        navPosition = agent.enterStorePosition.transform;
        
        timer = waitTime;
    }

    public override void UpdateState(StateManager agent)
    {
        SetDestination(agent);
       
        if (insideTheStore == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            agent.SwitchState(agent.walkToTableState);
        }
    }

    public void SetDestination(StateManager agent)
    {
        if (!NavMeshHelper.Validate(agent.navMeshAgent)) return;

        agent.walking = true;
    
        agent.navMeshAgent.destination = navPosition.position;
        
        if (Vector3.Distance(agent.navMeshAgent.nextPosition, navPosition.position) < 0.5f)
        {
            agent.walking = false;
            insideTheStore = true;
        }
    }
}
