using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : BaseState
{

    float rotationSpeed = 50f;
    float waitTime = 90;
    float timer = 0;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered WaitingState...");
        timer = waitTime;
        agent.isAnnoyed = false;
        agent.timeLeftOnOrder = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();


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

        // TODO: hur ska vi ge kunderna f�rdig mat?
        //       skapa en klickbar emote n�r mat �r klar?
        //       l�t kunder g� fram och plocka up?
        bool orderReady = agent.timeLeftOnOrder < (waitTime - 1);

        if (orderReady && Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
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
