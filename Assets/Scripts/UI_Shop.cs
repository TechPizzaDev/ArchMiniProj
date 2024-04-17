using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    Transform shopItemTemplate;
    IShopCustomer shopCustomer;
  

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Firer, Item.GetSprite(Item.ItemType.Firer), "Frier", Item.GetCost(Item.ItemType.Firer), 0);
        CreateItemButton(Item.ItemType.SadwichMaker, Item.GetSprite(Item.ItemType.SadwichMaker), "Sandwich", Item.GetCost(Item.ItemType.SadwichMaker), 1);

        Hide();
    }

    private void CreateItemButton(Item.ItemType itemType ,Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 90f;
    
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));

    }
    
    public void TryBuyItem(Item.ItemType itemType)
    {
        if(shopCustomer.TrySpendGoldAmount(Item.GetCost(itemType)))
        {
            // money to spend
            shopCustomer.BoughtItem(itemType);
        }
           
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);    
    }
}
