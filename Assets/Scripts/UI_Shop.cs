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
    public TMP_Text goldText;
    [SerializeField] Shoper shoper;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Ingridents, Item.GetSprite(Item.ItemType.Ingridents), "Ingridents", Item.GetCost(Item.ItemType.Ingridents), 0);
        CreateItemButton(Item.ItemType.Radio, Item.GetSprite(Item.ItemType.Radio), "Radio", Item.GetCost(Item.ItemType.Radio), 1);
        CreateItemButton(Item.ItemType.GoldenSpatula, Item.GetSprite(Item.ItemType.GoldenSpatula), "Golden Spatula", Item.GetCost(Item.ItemType.GoldenSpatula), 2);
        CreateItemButton(Item.ItemType.Marketing, Item.GetSprite(Item.ItemType.Marketing), "Marketing", Item.GetCost(Item.ItemType.Marketing), -1);

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
            goldText.text = "Gold " + shoper.gold;
        }           
    }
    public void UpdadteGolde()
    {
        goldText.text = "Gold " + shoper.gold;
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
