using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private Button _buyTab;
 

    [Header("Shopping Cart")]
    [SerializeField] private TextMeshProUGUI _basketTotalText;
    [SerializeField] private TextMeshProUGUI _playerGoldText;
    [SerializeField] private TextMeshProUGUI _shopGoldText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _buyButtonText;

    [Header("Item Preview Section")]
    [SerializeField] private Image _itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [SerializeField] private GameObject _itemListContentPanel;
    [SerializeField] private GameObject _shoppingCartContentPanel;


    private ShopSystem _shopSystem;
    private int _basketTotal;


    public void DisplayShopWindow(ShopSystem shopSystem )
    {
        _shopSystem = shopSystem;
      

        RefreshDisplay();
    }
    private void RefreshDisplay()
    {
        if (_buyButton != null)
        {
            _buyButtonText.text =  "Buy Items";
            _buyButton.onClick.RemoveAllListeners();
            
            //_buyButton.onClick.AddListener(BuyItems);
        }

        //ClearSlots();
        //ClearItemPreview();

        _basketTotalText.enabled = false;
        _buyButton.gameObject.SetActive(false);
        _basketTotal = 0;
     
        _shopGoldText.text = $"Shop Gold: {_shopSystem.AvailableGold}";

      
        DisplayShopInventory();

    }

    private void DisplayShopInventory()
    {
       
    }
}
