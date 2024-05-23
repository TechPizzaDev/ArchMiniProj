using System;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable

public class OrderState : BaseState
{
    public Func<GameObject?, EmoteEntry> announceOrder = null!;
    public Func<GameObject?, CustomerEmote> commitOrder = null!;

    float timer = 0;

    private EmoteEntry? orderEmote;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered OrderState");
        timer = agent.waitingForOrderTime;

        agent.SpawnTimerBar();
        agent.timerBar.SetMaxTime(agent.waitingForOrderTime);

        agent.timerBar.timerColor.color = agent.orderColor;

        if (orderEmote != null)
            orderEmote.Close(null);

        orderEmote = announceOrder.Invoke(agent.gameObject);
        orderEmote.OnClick += OrderEmote_OnClick;

        var emoteImage = orderEmote.GetImage();
        emoteImage.sprite = agent.orderSprite;
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
            agent.waitingState.emote = commitOrder.Invoke(agent.gameObject);
            agent.waitingState.emote.destroyOnClick = false;

            agent.SwitchState(agent.waitingState);
        }
    }
}
