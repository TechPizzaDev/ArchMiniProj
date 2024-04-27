using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingDownState : BaseState
{
    float waitTime = 2;
    float timer = 0;


    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered SittingDownState...");
        //agent.transform.position = new Vector2(agent.transform.position.x, agent.transform.position.y);
        timer = waitTime;

        agent.transform.rotation = Quaternion.identity;
    }


    public override void UpdateState(StateManager agent)
    {
        if (timer < 0)
        {
            agent.SwitchState(agent.orderState);

        }

        timer -= Time.deltaTime;
    }
}
