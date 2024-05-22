using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] UI_Shop uiShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IShopCustomer>(out var shopCustomer))
        {
            uiShop.Show(shopCustomer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IShopCustomer>(out var shopCustomer))
        {
            uiShop.Hide();
        }
    }
}
