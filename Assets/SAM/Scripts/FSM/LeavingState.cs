using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingState : BaseState
{
    float waitTime = 1;
    float timer = 0;
    bool leftTable = false;
    float moveSpeed = 5f;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered LeavingState...");
        timer = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {
        if (agent.isAnnoyed)
        {
            //l�gg in irriterad animation eller textur h�r.
        }
        else if (agent.isAngry)
        {
            //l�gg in arg animation eller textur h�r.
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (leftTable == false)
            {
                agent.seatManager[agent.int_chosenTable].FreeSeat();
                leftTable = true;
            }

            if (leftTable == true)
            {
                Vector2 targetPosition = agent.leavingStorePosition.transform.position;

                agent.transform.position = Vector2.MoveTowards(agent.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(agent.transform.position, targetPosition) < 0.1f)
                {
                    //agent d�r.
                }
            }
        }

        


    }
}
