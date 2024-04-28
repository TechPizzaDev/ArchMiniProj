using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoper : MonoBehaviour, IShopCustomer
{
    public int gold = 50;
    
    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("bought Item" + itemType);
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
