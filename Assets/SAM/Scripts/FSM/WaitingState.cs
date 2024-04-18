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
        else if(timer < 0)
        {
            agent.SwitchState(agent.standingUpState);
        }


        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
            {
                // Cast a ray from the mouse position
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                // Check if the ray hits this object and the object has a BoxCollider2D
                if (hit.collider != null && hit.collider.gameObject == agent.gameObject && agent.GetComponent<BoxCollider2D>() != null)
                {
                    agent.SwitchState(agent.eatingState);
                }
            }

    }

}
