using UnityEngine;
using UnityEngine.SceneManagement;

public class Shopper : MonoBehaviour, IShopCustomer
{
    public int gold = 50;
    LvlManager lvlManager;
    public static bool soundOn = false;
    public bool bellOn = false;
    // UI_Shop uiShop;
    [SerializeField] float incomeI = 1.1f;
    
    void Start()
    {
        lvlManager = FindAnyObjectByType<LvlManager>();
        // uiShop = FindAnyObjectByType<UI_Shop>();
    }

    public void BoughtItem(ItemType itemType)
    {
        Debug.Log("bought Item" + itemType);

        if (itemType == ItemType.Marketing)
        {
            lvlManager.customersThisLvl++;
        }
        else if (itemType == ItemType.Radio)
        {
            soundOn = true;
            
            lvlManager.ButtonFix(1);
            lvlManager.incomeIncrease = incomeI;
            // sett på musik
        }
        else if (itemType == ItemType.Bell)
        {
            bellOn = true;
            lvlManager.ButtonFix(4);
            // sett på musik
        }
        else if (itemType == ItemType.Ingridents)
        {
            lvlManager.ingridents++;
        }
        else if (itemType == ItemType.GoldenSpatula)
        {
            SceneManager.LoadSceneAsync(3);
            // Win the Game
        }
        lvlManager.UpdateText();
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if (gold >= spendGoldAmount)
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
