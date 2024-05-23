using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SysRandom = System.Random;

#nullable enable

public class OrderManager : MonoBehaviour
{
    private int orderCounter = 0;
    private SysRandom rng = new();

    public SceneRoot sceneRoot;
    public OrderQueue orderQueue;

    private List<IngredientPool> pools;

    class IngredientPool
    {
        public readonly Type type;
        public readonly List<Ingredient> ingredients = new();
        public readonly List<int> maxCounts = new();
        public int maxRecipeSize;

        public IngredientPool(Type type)
        {
            this.type = type;
        }
    }

    void Start()
    {
        var ingredients = sceneRoot.stationRoot.scene.GetRootGameObjects()
            .SelectMany(r => r.GetComponentsInChildren<Ingredient>())
            .GroupBy(i => i.GetType());

        Dictionary<Type, IngredientPool> lookup = new();

        foreach (var group in ingredients)
        {
            if (!lookup.TryGetValue(group.Key, out IngredientPool pool))
            {
                pool = new IngredientPool(group.Key);
                lookup.Add(group.Key, pool);
            }

            foreach (var ingredient in group)
            {
                PointList? pointList = ingredient.GetComponentInParent<PointList>();

                int maxCount = 1;
                if (pointList != null)
                    maxCount = pointList.points.Length;

                pool.ingredients.Add(ingredient);
                pool.maxCounts.Add(maxCount);
                pool.maxRecipeSize += maxCount;
            }
        }

        pools = lookup.Values.ToList();
    }

    public OrderDesc CreateOrder()
    {
        IngredientPool pool = pools[rng.Next(pools.Count)];

        var dict = new Dictionary<Ingredient, int>();

        int recipeSize = Math.Max((pool.maxRecipeSize + 2) / 3, rng.Next(pool.maxRecipeSize) + 1);

        int maxRetries = 2;

        for (int i = 0; i < recipeSize && maxRetries > 0; i++)
        {
            do
            {
                int ingIndex = rng.Next(pool.ingredients.Count);
                Ingredient ing = pool.ingredients[ingIndex];

                if (!dict.TryGetValue(ing, out int ingCount))
                {
                    ingCount = 0;
                }

                if (ingCount >= pool.maxCounts[ingIndex])
                {
                    if (maxRetries > 0)
                    {
                        maxRetries--;

                        // Retry picking ingredients if we can.
                        continue;
                    }
                    break;
                }

                dict[ing] = ingCount + 1;
                break;
            }
            while (true);
        }

        int id = ++orderCounter;

        var order = new OrderDesc(id, dict);

        orderQueue.Enqueue(order);

        return order;
    }
}
