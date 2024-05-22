using UnityEngine;

public class EatingState : BaseState
{    
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered EatingState...");

        timer = agent.eatingTime;
        agent.timerBar.timerColor.color = agent.eatingColor;
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
