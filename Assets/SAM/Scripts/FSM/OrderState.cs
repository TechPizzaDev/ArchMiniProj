using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class OrderState : BaseState
{

    float rotationSpeed = 75f;
    float waitTime = 30;
    float timer = 0;
    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered OrderState");
        timer = waitTime;
        //Implementera order animation/Textur.
    }


    public override void UpdateState(StateManager agent)
    {
        agent.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);



        timer -= Time.deltaTime;

        if (timer < waitTime/2 && timer > 0)
        {
            agent.isAnnoyed = true;
        }



        if(timer < 0)
        {
            agent.SwitchState(agent.standingUpState);
        }


        //DETTA ÄR EXEMPELKOD PÅ EN ORDER VISUALISERING, KOMMER ATT ÄNDRAS.

        agent.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);


        //klicka på agenten för att ta emot order.
        if (Input.GetMouseButtonDown(0)) 
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            
            if (hit.collider != null && hit.collider.gameObject == agent.gameObject && agent.GetComponent<BoxCollider2D>() != null)
            {
                agent.SwitchState(agent.waitingState);
            }
        }
    }
}
