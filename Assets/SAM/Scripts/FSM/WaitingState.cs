using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : BaseState
{

    float rotationSpeed = 50f;
    
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered WaitingState...");
        timer = agent.waitingTime;
        agent.isAnnoyed = false;
        agent.timeLeftOnOrder = timer;
    }


    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();
        //agent.timerBar.SetTime(timer);

        timer -= Time.deltaTime;
        agent.timeLeftOnOrder -= Time.deltaTime;


        agent.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);



        if (timer < 60 && timer > 30)
        {
            rotationSpeed = 150f;
            //implementera irriterad textur/animation

            agent.isAnnoyed = true;

        }
        else if (timer < 30 && timer > 0)
        {
            rotationSpeed = 250;
            //implementera arg textur/animation
            agent.isAnnoyed = false;
            agent.isAngry = true;
        }
        else if (timer < 0)
        {
            agent.SwitchState(agent.standingUpState);
        }

        // TODO: hur ska vi ge kunderna färdig mat?
        //       skapa en klickbar emote när mat är klar?
        //       låt kunder gå fram och plocka up?

        // Sam: När ordern är färdig, håller spelaren i tallriken, och leverar maten till kunden,
        // genom att klicka på rätt kund och vänta tills kunden når fram.
        // Endast då blir "orderDelivered" true.

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
