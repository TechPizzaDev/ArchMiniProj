using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnteringState : BaseState
{
    float moveSpeed = 5f;
    float waitTime = 2;
    float timer = 0;
    bool insideTheStore = false;
    Vector2 storePosition;
    Transform navPosition;


    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered EnteringState...");
        navPosition = agent.enterStorePosition.transform;
        storePosition = navPosition.position;
        timer = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {

        //SetDestination(agent);
        agent.transform.position = Vector2.MoveTowards(agent.transform.position, storePosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(agent.transform.position, storePosition) < 0.1f)
        {
            insideTheStore = true;

        }

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

        if (agent.navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent reference is null. Make sure it's properly initialized.");
            return;
        }

        if (!agent.navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh surface.");
            return;
        }

        // Set the destination of the NavMeshAgent to the next patrol point
        agent.navMeshAgent.destination = navPosition.position;


        if (Vector3.Distance(agent.navMeshAgent.nextPosition, navPosition.position) < 0.1f)
        {

            agent.SwitchState(agent.walkToTableState);

        }


    }
}
