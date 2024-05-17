using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingState : BaseState
{
    float rotationSpeed = 50f;
    float waitTime = 15;
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered EatingState...");

        //Implementera ätanimation.
        //stäng av eventuell annoyed/angry state.
        timer = agent.eatingTime;
        agent.timerBar.timerColor.color = agent.lightBlue;
        agent.timerBar.SetMaxTime(agent.eatingTime);
    }


    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();

        agent.timerBarInstance.transform.position = agent.transform.position + agent.popupPosition;

        agent.timerBar.SetTime(timer);

        timer -= Time.deltaTime;



        if (timer < 0)
        {
            agent.SwitchState(agent.standingUpState);
        }
    }
}
