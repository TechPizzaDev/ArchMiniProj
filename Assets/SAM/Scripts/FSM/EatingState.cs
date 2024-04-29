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
        timer = waitTime;
        agent.isAnnoyed = false;
        agent.isAngry = false;
    }


    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();

        agent.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            agent.SwitchState(agent.standingUpState);
        }
    }
}
