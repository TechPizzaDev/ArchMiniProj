using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkToTableState : BaseState
{

    Transform[] tables;
    Transform chosenTable;
    float moveSpeed = 5f;
    bool tableFound = false;
    
    
    

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered WalkToTableState...");


        //tables = agent.tables;
        //System.Random random = new System.Random();
        //int randomNr = random.Next(0, 4);
        //chosenTable = tables[randomNr];
    }


    public override void UpdateState(StateManager agent)
    {

        if(tableFound == false)
        for (int i = 0; i < agent.tables.Length; i++)
        {

            if (agent.tableManager[i].SeatAvailable() == true)
            {
                chosenTable = agent.tables[i];
                agent.tableManager[i].OccupySeat();
                tableFound = true;

                break;
            }
        }


        if(tableFound == true)
        {
            Vector2 targetPosition = chosenTable.position;

            agent.transform.position = Vector2.MoveTowards(agent.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(agent.transform.position, targetPosition) < 0.1f)
            {
                agent.SwitchState(agent.sittingState);
            }

        }


        

    }

}

