using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    
    public enum ItemType
    {
        Ingridents,
        Radio,
        Marketing,
        GoldenSpatula,
        Bell,           

    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default: 
                case ItemType.Ingridents: return 100;
                case ItemType.Radio: return 500;
                case ItemType.Marketing: return 50;
                case ItemType.GoldenSpatula: return 2000;
                case ItemType.Bell: return 300;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.Ingridents: return GameAssets.i.Ingridents;
            case ItemType.Radio: return GameAssets.i.Radio;
            case ItemType.Marketing: return GameAssets.i.Marketing;
            case ItemType.GoldenSpatula: return GameAssets.i.GoldenSpatula;
            case ItemType.Bell: return GameAssets.i.Bell;
        }
    }
}
