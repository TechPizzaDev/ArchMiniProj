using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class OrderState : BaseState
{
    public event Action<EmoteEntry>? OnAnnounceOrder;
    public event Action<OrderEntry>? OnCommitOrder;

    float rotationSpeed = 75f;
    float waitTime = 15;
    float timer = 0;

    private EmoteEntry? orderEmote;

    public override void EnterState(StateManager agent)
    {
        Debug.Log("Entered OrderState");
        timer = waitTime;
        //Implementera order animation/Textur.

        if (orderEmote != null)
            orderEmote.Close(false);

        orderEmote = new EmoteEntry(agent.gameObject);
        orderEmote.OnClick += OrderEmote_OnClick;
        orderEmote.OnClose += OrderEmote_OnClose;
        OnAnnounceOrder?.Invoke(orderEmote);
    }

    public override void UpdateState(StateManager agent)
    {
        



        timer -= Time.deltaTime;

        if (timer < waitTime / 2 && timer > 0)
        {
            agent.isAnnoyed = true;
        }



        if (timer < 0)
        {
            if (orderEmote != null)
                orderEmote.Close(false);

            agent.SwitchState(agent.standingUpState);
        }



    }

    private void OrderEmote_OnClick(EmoteEntry emote, bool userAction)
    {


        emote.Close(true);

        
    }

    private void OrderEmote_OnClose(EmoteEntry emote, bool userAction)
    {
        if (!userAction)
        {
            return;
        }

        var agent = emote.Source.GetComponent<StateManager>();

        OnCommitOrder?.Invoke(new OrderEntry(agent));

        agent.SwitchState(agent.waitingState);
    }
}
