
public interface IShopCustomer 
{
    void BoughtItem(ItemType itemType);

    bool TrySpendGoldAmount(int goldAmount);
}
