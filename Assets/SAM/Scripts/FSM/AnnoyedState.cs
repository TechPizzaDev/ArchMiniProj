using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoyedState : BaseState
{
    float rotationSpeed = 150f;
    float waitTime = 30;
    float timer = 0;
    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered AnnoyedState...");
        timer = waitTime;
    }


    public override void UpdateState(StateManager agent)
    {
        agent.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        timer -= Time.deltaTime;


        if (timer < 0)
        {
            //agent.SwitchState(agent.angryState);

        }



        if (Input.GetMouseButtonDown(0)) 
        {
            
            RaycastHit2D hit = RayHelper.RaycastFromCamera(Input.mousePosition);

            
            if (hit.collider != null && hit.collider.gameObject == agent.gameObject && agent.GetComponent<BoxCollider2D>() != null)
            {
                agent.SwitchState(agent.eatingState);
            }
        }

    }
}
