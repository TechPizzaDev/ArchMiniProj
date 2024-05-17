using System.Collections.Generic;
using UnityEngine;

//MMM = Mack Maker Manager ;)

public class SandwichManager : MonoBehaviour
{
    GameObject objectInHand;

    public GameObject visualPrefab;
    public VisualSandwich currentSandwich;
    public VisualBlender currentSmoothie;
    public GameObject orderedSandwich;
    public GameObject orderedSmoothie;

    public Dictionary<Ingredient, List<GameObject>> sandwichIngredients = new();

    public Dictionary<Ingredient, List<GameObject>> smoothieIngredients = new();

    VisualSandwich SpawnVisual()
    {
        return Instantiate(visualPrefab, transform).GetComponent<VisualSandwich>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSandwich = SpawnVisual();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = RayHelper.RaycastFromCamera(Input.mousePosition);

        if (hit.collider != null && Input.GetMouseButtonDown(0))
        {
            Debug.Assert(objectInHand == null);

            if (hit.collider.gameObject.TryGetComponent(out IngredientSandwich ingredientSandwich))
            {
                objectInHand = Instantiate(ingredientSandwich.prefab, currentSandwich.placeArea.gameObject.transform, true);
                objectInHand.AddComponent<IngredientHolder>().value = ingredientSandwich;
            }
            else if (hit.collider.gameObject.TryGetComponent(out IngredientSmoothie ingredientSmoothie))
            {
                objectInHand = Instantiate(ingredientSmoothie.prefab, currentSmoothie.transform, true);
                objectInHand.AddComponent<IngredientHolder>().value = ingredientSmoothie;
                objectInHand.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else if (hit.collider.gameObject.TryGetComponent(out TrashCan trash))
            {
                // TODO: clear only current screen
                DestroyObjectsInDict(sandwichIngredients);
                DestroyObjectsInDict(smoothieIngredients);
            }
        }

        if (!Input.GetMouseButton(0))
        {
            if (objectInHand != null)
            {
                Dictionary<Ingredient, List<GameObject>> selectedDict = null;
                Collider2D selectedArea = null;

                Ingredient ingredient = objectInHand.GetComponent<IngredientHolder>().value;
                if (ingredient is IngredientSandwich)
                {
                    selectedDict = sandwichIngredients;
                    selectedArea = currentSandwich.placeArea;
                }
                else if (ingredient is IngredientSmoothie)
                {
                    selectedDict = smoothieIngredients;
                }

                if (hit.collider == selectedArea && TryPlace(selectedDict, selectedArea == null ? Vector3.zero : selectedArea.transform.position, objectInHand))
                {
                }
                else if (ingredient is IngredientSandwich)
                {
                    Destroy(objectInHand);
                }

                objectInHand.GetComponent<Rigidbody2D>().isKinematic = false;

                objectInHand = null;
            }
        }

        if (objectInHand != null)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -0.1f;
            objectInHand.transform.position = pos;
        }
    }

    public void AddIngredientObject(GameObject obj)
    {
        Dictionary<Ingredient, List<GameObject>> dict = null;

        Ingredient ingredient = obj.GetComponent<IngredientHolder>().value;
        if (ingredient is IngredientSandwich)
        {
            dict = sandwichIngredients;
        }
        else if (ingredient is IngredientSmoothie)
        {
            dict = smoothieIngredients;
        }

        if (dict == null)
            return;

        if (!dict.TryGetValue(ingredient, out var ingdList))
        {
            ingdList = new List<GameObject>();
            dict.Add(ingredient, ingdList);
        }

        ingdList.Add(obj);
    }

    public bool RemoveIngredientObject(GameObject obj)
    {
        Dictionary<Ingredient, List<GameObject>> dict = null;

        Ingredient ingredient = obj.GetComponent<IngredientHolder>().value;
        if (ingredient is IngredientSandwich)
        {
            dict = sandwichIngredients;
        }
        else if (ingredient is IngredientSmoothie)
        {
            dict = smoothieIngredients;
        }

        if (dict != null && dict.TryGetValue(ingredient, out List<GameObject> ingdList))
        {
            return ingdList.Remove(obj);
        }
        return false;
    }

    bool TryPlace(Dictionary<Ingredient, List<GameObject>> dict, Vector3 position, GameObject objToPlace)
    {
        var ingredient = objectInHand.GetComponent<IngredientHolder>().value;
        if (!dict.TryGetValue(ingredient, out var ingdList))
        {
            ingdList = new List<GameObject>();
            dict.Add(ingredient, ingdList);
        }

        if (!objToPlace.TryGetComponent(out PointList pointList))
        {
            ingdList.Add(objToPlace);
            return true;
        }

        if (ingdList.Count < pointList.points.Length)
        {
            ingdList.Add(objToPlace);

            Vector2 point = pointList.points[(ingdList.Count - 1) % pointList.points.Length];
            objToPlace.transform.position = position + new Vector3(point.x, point.y, 0);
            return true;
        }
        return false;
    }

    private static void FinishedItem(GameObject prefab, Dictionary<Ingredient, List<GameObject>> dict)
    {
        if (dict.Count == 0)
        {
            return;
        }

        GameObject instance = Instantiate(prefab);
        instance.GetComponent<OrderedItem>().ingredients = dict;

        DestroyObjectsInDict(dict);
    }

    public void FinishedSandwich()
    {
        FinishedItem(orderedSandwich, sandwichIngredients);

        currentSandwich.isFinished = true;
        currentSandwich.rigidBody.AddForce(new Vector2(0, 1000));
        currentSandwich = SpawnVisual();
    }

    public void FinishedSmoothie()
    {
        FinishedItem(orderedSmoothie, smoothieIngredients);

        Debug.Log("Finished smoothie.");
    }

    public static void DestroyObjectsInDict(Dictionary<Ingredient, List<GameObject>> dict)
    {
        foreach (var list in dict.Values)
        {
            foreach (var obj in list.ToArray())
            {
                Destroy(obj);
            }
        }
    }
}
