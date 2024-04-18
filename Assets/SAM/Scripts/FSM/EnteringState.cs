using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnteringState : BaseState
{
    float moveSpeed = 5f;
    float waitTime = 2;
    float timer = 0;
    bool insideTheStore = false;
    Vector2 storeLine;


    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered EnteringState...");
        storeLine = new Vector2(-6, 1);
        timer = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {

        
        agent.transform.position = Vector2.MoveTowards(agent.transform.position, storeLine, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(agent.transform.position, storeLine) < 0.1f)
        {
            insideTheStore = true;
            
        }

        if(insideTheStore == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            agent.SwitchState(agent.walkToTableState);
        }
    }
}
