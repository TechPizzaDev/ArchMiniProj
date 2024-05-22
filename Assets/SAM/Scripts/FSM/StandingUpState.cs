using UnityEngine;

public class StandingUpState : BaseState
{
    float waitTime = 2;
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered StandingUpState...");
        timer = waitTime;

        agent.transform.rotation = Quaternion.identity;

        //stäng av ät animation.
        agent.DestroyTimeBar();
    }

    public override void UpdateState(StateManager agent)
    {
        if (timer < 0)
        {
            agent.SwitchState(agent.leavingState);
        }

        timer -= Time.deltaTime;
    }
}
