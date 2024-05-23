using System.Collections.Generic;

public class OrderDesc
{
    public readonly int id;
    public readonly Dictionary<Ingredient, int> ingredients;

    public OrderDesc(int id, Dictionary<Ingredient, int> ingredients)
    {
        this.id = id;
        this.ingredients = ingredients;
    }
}