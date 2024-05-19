using System;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable

public class OrderState : BaseState
{
    public Func<GameObject?, EmoteEntry> announceOrder = null!;
    public Func<GameObject?, EmoteEntry> commitOrder = null!;



    float timer = 0;

    private EmoteEntry? orderEmote;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered OrderState");
        timer = agent.waitingForOrderTime;


        agent.SpawnTimerBar();
        agent.timerBar.SetMaxTime(agent.waitingForOrderTime);
        agent.timerBar.timerColor.color = agent.purple;


        if (orderEmote != null)
            orderEmote.Close(null);

        orderEmote = announceOrder.Invoke(agent.gameObject);
        orderEmote.OnClick += OrderEmote_OnClick;
    }

    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();


        timer -= Time.deltaTime;

        agent.timerBar.SetTime(timer);
        agent.timerBarInstance.transform.position = agent.transform.position + agent.popupPosition;



        if (timer < 0)
        {
            if (orderEmote != null)
                orderEmote.Close(null);

            agent.SwitchState(agent.standingUpState);
        }
    }

    private void OrderEmote_OnClick(EmoteEntry emote, PointerEventData? eventData)
    {
        var agent = emote.Source == null ? null : emote.Source.GetComponent<StateManager>();
        if (agent != null)
        {
            commitOrder.Invoke(agent.gameObject);

            agent.SwitchState(agent.waitingState);
        }
    }
}
