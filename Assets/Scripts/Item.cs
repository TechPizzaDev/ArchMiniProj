using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    
    public enum ItemType
    {
        Firer,
        SadwichMaker,

    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default: 
                case ItemType.Firer: return 20;
                case ItemType.SadwichMaker: return 30;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.Firer: return GameAssets.i.Frier;
            case ItemType.SadwichMaker: return GameAssets.i.Head;
        }
    }
}
