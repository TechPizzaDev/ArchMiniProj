using UnityEngine;

public abstract class Ingredient : MonoBehaviour
{
    public GameObject prefab;

    public string nameOverride;

    public abstract string productName { get; }

    public virtual string ingredientName => string.IsNullOrEmpty(nameOverride) ? prefab.name : nameOverride;
}
