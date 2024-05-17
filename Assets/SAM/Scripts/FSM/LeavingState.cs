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
        agent.walking = true;

    }


    public override void UpdateState(StateManager agent)
    {
        


        if (agent.isAnnoyed)
        {
            //l�gg in irriterad animation eller textur h�r.
        }
        else if (agent.isAngry)
        {
            //l�gg in arg animation eller textur h�r.
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

            }
        }

        if (Vector3.Distance(agent.navMeshAgent.nextPosition, navPosition.position) < 1f)
        {

            agent.DestroyCustomer();

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



    }
}
