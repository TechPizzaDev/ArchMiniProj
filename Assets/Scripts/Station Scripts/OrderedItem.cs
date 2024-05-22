using System.Collections.Generic;
using UnityEngine;

public abstract class OrderedItem : MonoBehaviour
{
    public Dictionary<Ingredient, List<GameObject>> ingredients;
}
