using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoper : MonoBehaviour, IShopCustomer
{
    public int gold = 50;
    LvlManager lvlManager;
    public bool soundOn = false;
    public bool bellOn = false;
    UI_Shop uiShop;
    [SerializeField] float incomeI = 1.1f;
    void Start()
    {
        lvlManager = FindAnyObjectByType<LvlManager>();
        uiShop = FindAnyObjectByType<UI_Shop>();
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
            soundOn = true;
            uiShop.HideButton(1);
            lvlManager.incomeIncrease = incomeI;
            // sett på musik
        }
        else if (itemType == Item.ItemType.Bell)
        {
            bellOn = true;
            uiShop.HideButton(4);
            // sett på musik
        }
        else if (itemType == Item.ItemType.Ingridents)
        {
            lvlManager.ingridents++;
        }
        else if (itemType == Item.ItemType.GoldenSpatula)
        {
            SceneManager.LoadSceneAsync(2);
            // Win the Game
        }
        lvlManager.UpdateText();
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
