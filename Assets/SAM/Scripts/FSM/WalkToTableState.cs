using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkToTableState : BaseState
{

    
    Transform chosenSeat;
    float moveSpeed = 5f;
    bool tableFound = false;
    
    
    

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered WalkToTableState...");


        
    }


    public override void UpdateState(StateManager agent)
    {

        if(tableFound == false)
        for (int i = 0; i < agent.seats.Length; i++)
        {

            if (agent.seatManager[i].SeatAvailable() == true)
            {
                chosenSeat = agent.seats[i];
                agent.seatManager[i].OccupySeat();
                agent.int_chosenTable = i;
                tableFound = true;
                

                break;
            }
        }


        if(tableFound == true)
        {
            //Vector2 targetPosition = chosenSeat.position;

            //agent.transform.position = Vector2.MoveTowards(agent.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            //if (Vector2.Distance(agent.transform.position, targetPosition) < 0.1f)
            //{
            //    agent.SwitchState(agent.sittingState);
            //}
            SetDestination(agent);
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

            
            agent.navMeshAgent.destination = new Vector3(chosenSeat.position.x, chosenSeat.position.y + 0.4f, chosenSeat.position.z);

            if (Vector3.Distance(agent.navMeshAgent.nextPosition, chosenSeat.position) < 1f)
            {
                
                agent.SwitchState(agent.sittingState);

            }

        }

    }

}

