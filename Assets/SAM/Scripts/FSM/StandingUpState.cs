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
        //stäng av ät animation.
        agent.DestroyTimeBar();

    }


    public override void UpdateState(StateManager agent)
    {


        if(agent.isAnnoyed==true)
        {
            //lägg in irriterad animation eller textur här.
        }
        else if (agent.isAngry==true)
        {
            //lägg in arg animation/textur.
        }

        if (timer < 0)
        {
            agent.SwitchState(agent.leavingState);

        }

        timer -= Time.deltaTime;
    }
}
