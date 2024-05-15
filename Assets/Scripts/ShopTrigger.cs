using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] UI_Shop uiShop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();
        if(shopCustomer != null )
        {
            uiShop.Show(shopCustomer);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            uiShop.Hide();
        }
    }
}
