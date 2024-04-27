using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeavingState : BaseState
{
    float waitTime = 1;
    float timer = 0;
    bool leftTable = false;
    float moveSpeed = 5f;
    Transform navPosition;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered LeavingState...");
        timer = waitTime;
        navPosition = agent.leavingStorePosition.transform;
    }


    public override void UpdateState(StateManager agent)
    {
        if (agent.isAnnoyed)
        {
            //lägg in irriterad animation eller textur här.
        }
        else if (agent.isAngry)
        {
            //lägg in arg animation eller textur här.
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (leftTable == false)
            {
                agent.seatManager[agent.int_chosenTable].FreeSeat();
                leftTable = true;
            }

            if (leftTable == true)
            {
                SetDestination(agent);

                //Vector2 targetPosition = agent.leavingStorePosition.transform.position;

                //agent.transform.position = Vector2.MoveTowards(agent.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                //if (Vector2.Distance(agent.transform.position, targetPosition) < 0.1f)
                //{
                //    //agent dör.
                //}
            }
        }


    }
    void SetDestination(StateManager agent)
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


        agent.navMeshAgent.destination = navPosition.position;


        if (Vector3.Distance(agent.navMeshAgent.nextPosition, navPosition.position) < 0.5f)
        {

            agent.SelfDestruct();

        }

    }
}
