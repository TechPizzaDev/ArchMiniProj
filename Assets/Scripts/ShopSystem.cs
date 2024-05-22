using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public bool shopping;

    [SerializeField] private int _availableGold;

    public int AvailableGold => _availableGold;

    public ShopSystem(int size, int gold)
    {
        _availableGold = gold;
    }

    private void ReduceGold(int price)
    {
        _availableGold -= price;
    }

    public void GainGold(int gainTotal)
    {
        _availableGold += gainTotal;
    }
}
