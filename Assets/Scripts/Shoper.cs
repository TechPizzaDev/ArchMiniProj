using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoper : MonoBehaviour, IShopCustomer
{
    public int gold = 50;
    LvlManager lvlManager;

    void Start()
    {
        lvlManager = FindAnyObjectByType<LvlManager>();
    }
    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("bought Item" + itemType);

        if(itemType == Item.ItemType.Marketing)
        {
            lvlManager.customersThisLvl++;

        }
        else if(itemType == Item.ItemType.Radio)
        {
            // sett på musik
        }
        else if (itemType == Item.ItemType.Ingridents)
        {
            lvlManager.ingridents++;
        }
        else if (itemType == Item.ItemType.GoldenSpatula)
        {
            // Win the Game
        }
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if(gold >= spendGoldAmount)
        {
            gold -= spendGoldAmount;            
            return true;
        }
        else
        {
            return false;
        }
    }

   
}
