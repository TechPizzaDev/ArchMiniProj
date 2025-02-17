using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaitingState : BaseState
{
    public CustomerEmote emote;

    float timer = 0;
    Image emoteImage;

    bool waitingEmoteClicked;

    public override void EnterState(StateManager agent)
    {
        //Debug.Log("Entered WaitingState...");
        timer = agent.waitingForFoodTime;
        emoteImage = emote.GetImage();

        agent.timeLeftOnOrder = timer;

        agent.timerBar.SetMaxTime(agent.waitingForFoodTime);

        agent.timerBar.timerColor.color = agent.waitingColor;
        emoteImage.sprite = agent.waitingSprite;

        emote.OnClick += Emote_OnClick;
    }

    private void Emote_OnClick(EmoteEntry entry, PointerEventData eventData)
    {
        if (eventData == null) return;

        waitingEmoteClicked = true;
    }

    public override void UpdateState(StateManager agent)
    {
        agent.SittingDirection();

        //TimerBar Position

        agent.timerBarInstance.transform.position = agent.transform.position + agent.popupPosition;


        timer -= Time.deltaTime;
        agent.timeLeftOnOrder -= Time.deltaTime;


        agent.timerBar.SetTime(timer);

        if (timer < (agent.waitingForFoodTime * 0.75f) && timer > (agent.waitingForFoodTime / 3))
        {
            agent.timerBar.timerColor.color = agent.annoyedColor;
            emoteImage.sprite = agent.annoyedSprite;
        }
        else if (timer < (agent.waitingForFoodTime / 3) && timer > 0)
        {
            agent.timerBar.timerColor.color = agent.angryColor;
            emoteImage.sprite = agent.angrySprite;
        }
        else if (timer <= 0)
        {
            CloseOrder(agent);

            agent.SwitchState(agent.standingUpState);

            SoundManager.Instance.NoSound.Play();
            return;
        }

        // TODO: hur ska vi ge kunderna f�rdig mat?
        //       skapa en klickbar emote n�r mat �r klar?
        //       l�t kunder g� fram och plocka up?

        // Sam: N�r ordern �r f�rdig, h�ller spelaren i tallriken, och leverar maten till kunden,
        // genom att klicka p� r�tt kund och v�nta tills kunden n�r fram.
        // Endast d� blir "orderDelivered" true.

        if (waitingEmoteClicked)
        {
            TryGrabOrder(agent);
            waitingEmoteClicked = false;
        }
    }

    private void TryGrabOrder(StateManager agent)
    {
        if (!agent.orderStack.TryPeek(out OrderedItem topItem))
        {
            SoundManager.Instance.NoSound.Play();
            return;
        }

        if (!CompareIng(emote.orderDesc.ingredients, topItem.ingredients))
        {
            SoundManager.Instance.NoSound.Play();
            return;
        }

        agent.orderStack.TryPop(out OrderedItem poppedItem);
        Debug.Assert(poppedItem == topItem);

        Object.Destroy(poppedItem.gameObject);

        CloseOrder(agent);

        agent.SwitchState(agent.eatingState);

        SoundManager.Instance.YesSound.Play();
    }

    private void CloseOrder(StateManager agent)
    {
        agent.orderQueue.Remove(emote.orderDesc);

        if (emote != null)
            emote.Close(null);
    }

    private bool CompareIng(
        Dictionary<Ingredient, int> left,
        Dictionary<Ingredient, List<GameObject>> right)
    {
        if (left.Count != right.Count)
            return false;

        foreach ((Ingredient leftKey, int leftValue) in left)
        {
            if (!right.TryGetValue(leftKey, out List<GameObject> rightValue) || leftValue != rightValue.Count)
            {
                return false;
            }
        }

        return true;
    }
}
