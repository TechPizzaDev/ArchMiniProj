using UnityEngine;

public class SittingDownState : BaseState
{
    float waitTime = 2;
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered SittingDownState...");

        timer = waitTime;

        agent.transform.rotation = Quaternion.identity;
        agent.walking = false;
    }

    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();

        if (timer < 0)
        {
            agent.SwitchState(agent.orderState);
        }

        timer -= Time.deltaTime;
    }
}
