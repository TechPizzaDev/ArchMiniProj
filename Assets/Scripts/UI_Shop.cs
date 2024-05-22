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
    public TMP_Text ingridentsText;
    [SerializeField] Shopper shoper;
    LvlManager lvlManager;
    private List<Button> itemButtons = new();

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);
        lvlManager = FindAnyObjectByType<LvlManager>();
    }

    private void Start()
    {
        CreateItemButton(ItemType.Ingridents, Item.GetSprite(ItemType.Ingridents), "Ingridents", Item.GetCost(ItemType.Ingridents), 0);
        CreateItemButton(ItemType.Radio, Item.GetSprite(ItemType.Radio), "Radio", Item.GetCost(ItemType.Radio), 1);
        CreateItemButton(ItemType.GoldenSpatula, Item.GetSprite(ItemType.GoldenSpatula), "Golden Spatula", Item.GetCost(ItemType.GoldenSpatula), 2);
        CreateItemButton(ItemType.Marketing, Item.GetSprite(ItemType.Marketing), "Marketing", Item.GetCost(ItemType.Marketing), -1);
        CreateItemButton(ItemType.Bell, Item.GetSprite(ItemType.Bell), "Bell", Item.GetCost(ItemType.Bell), -2);

        Hide();
    }

    private void CreateItemButton(ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 90f;

        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        Button button = shopItemTransform.GetComponent<Button>();
        button.onClick.AddListener(() => TryBuyItem(itemType));

        itemButtons.Add(button);  // Lägg till knappen till listan
    }

    public void TryBuyItem(ItemType itemType)
    {
        if (shopCustomer.TrySpendGoldAmount(Item.GetCost(itemType)))
        {
            // money to spend
            SoundManager.Instance.BuySound.Play();

            shopCustomer.BoughtItem(itemType);
            goldText.text = "Gold " + shoper.gold;
            ingridentsText.text = " " + lvlManager.ingridents;
        }
        else
        {
            SoundManager.Instance.DeclineSound.Play();
        }
    }

    public void UpdadtNumbers()
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

    public void HideButton(int index)
    {
        if (index >= 0 && index < itemButtons.Count)
        {
            itemButtons[index].gameObject.SetActive(false);
        }
    }
}
