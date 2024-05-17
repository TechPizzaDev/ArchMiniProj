using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WaitingState : BaseState
{

    float rotationSpeed = 50f;
    
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered WaitingState...");
        timer = agent.waitingTime;
        
        agent.timeLeftOnOrder = timer;
        agent.SpawnTimerBar();
        agent.timerBar.SetMaxTime(agent.waitingTime);
    }


    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();

        //TimerBar Position

        agent.timerBarInstance.transform.position = agent.transform.position + agent.popupPosition;


        timer -= Time.deltaTime;
        agent.timeLeftOnOrder -= Time.deltaTime;


        agent.timerBar.SetTime(timer);

        if (timer < (agent.waitingTime*0.75f) && timer > (agent.waitingTime/3))
        {

            agent.timerBar.timerColor.color = Color.yellow;

        }
        else if (timer < (agent.waitingTime/3) && timer > 0)
        {
            agent.timerBar.timerColor.color = Color.red;
            
        }
        else if (timer <= 0)
        {
            agent.SwitchState(agent.standingUpState);
        }

        // TODO: hur ska vi ge kunderna f�rdig mat?
        //       skapa en klickbar emote n�r mat �r klar?
        //       l�t kunder g� fram och plocka up?

        // Sam: N�r ordern �r f�rdig, h�ller spelaren i tallriken, och leverar maten till kunden,
        // genom att klicka p� r�tt kund och v�nta tills kunden n�r fram.
        // Endast d� blir "orderDelivered" true.

        bool orderDelivered = true;

        if (orderDelivered && Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {
            // Cast a ray from the mouse position
            RaycastHit2D hit = RayHelper.RaycastFromCamera(Input.mousePosition);

            // Check if the ray hits this object and the object has a BoxCollider2D
            if (hit.collider != null && hit.collider.gameObject == agent.gameObject && agent.GetComponent<BoxCollider2D>() != null)
            {
                agent.SwitchState(agent.eatingState);
            }
        }

    }

}
