using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnteringState : BaseState
{

    float waitTime = 2;
    float timer = 0;


    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered EnteringState...");
        agent.transform.position = new Vector2(-8, 0);
        timer = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {
        if (timer < 0)
        {
            agent.SwitchState(agent.walkToTableState);
            
        }

        timer -= Time.deltaTime;
    }
}
