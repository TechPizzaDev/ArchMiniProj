using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingUpState : BaseState
{

    float waitTime = 2;
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered StandingUpState...");
        timer = waitTime;

        agent.transform.rotation = Quaternion.identity;
        //st�ng av �t animation.
        agent.DestroyTimeBar();

    }


    public override void UpdateState(StateManager agent)
    {


        if(agent.isAnnoyed==true)
        {
            //l�gg in irriterad animation eller textur h�r.
        }
        else if (agent.isAngry==true)
        {
            //l�gg in arg animation/textur.
        }

        if (timer < 0)
        {
            agent.SwitchState(agent.leavingState);

        }

        timer -= Time.deltaTime;
    }
}
