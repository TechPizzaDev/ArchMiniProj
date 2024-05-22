using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    GameObject objectInHand;

    public GameObject visualPrefab;
    public VisualSandwich currentSandwich;
    public VisualBlender blender;
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
                objectInHand = Instantiate(ingredientSmoothie.prefab, blender.transform, true);
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
                    objectInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                }

                Vector3 placePos = selectedArea == null ? Vector3.zero : selectedArea.transform.position;
                if (hit.collider == selectedArea && TryPlace(selectedDict, placePos, objectInHand))
                {
                }
                else if (ingredient is IngredientSandwich)
                {
                    Destroy(objectInHand);
                }

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

    private static bool FinishedItem(GameObject prefab, ref Dictionary<Ingredient, List<GameObject>> dict)
    {
        if (dict.Count == 0)
        {
            return false;
        }

        GameObject instance = Instantiate(prefab);
        instance.GetComponent<OrderedItem>().ingredients = dict;

        dict = new Dictionary<Ingredient, List<GameObject>>();
        return true;
    }

    public void FinishedSandwich()
    {
        if (!FinishedItem(orderedSandwich, ref sandwichIngredients))
        {
            return;
        }

        var renderer = currentSandwich.GetComponentInChildren<SpriteRenderer>();
        renderer.sortingOrder += 1;

        currentSandwich.isFinished = true;
        currentSandwich.rigidBody.AddForce(new Vector2(0, 1000));

        currentSandwich = SpawnVisual();
    }

    public void FinishedSmoothie()
    {
        Dictionary<Ingredient, List<GameObject>> previousDict = smoothieIngredients;

        if (!FinishedItem(orderedSmoothie, ref smoothieIngredients))
        {
            return;
        }

        var spriteList = new List<(Color startColor, SpriteRenderer sprite)>();
        foreach (var list in previousDict.Values)
        {
            foreach (var obj in list)
            {
                Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
                rigid.isKinematic = true;
                rigid.velocity = new Vector2(0, 0);

                foreach (var collider in obj.GetComponentsInChildren<Collider2D>())
                {
                    collider.enabled = false;
                }

                foreach (var sprite in obj.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteList.Add((sprite.color, sprite));
                }
            }
        }

        blender.PlayBlending(progress =>
        {
            foreach ((Color startColor, SpriteRenderer sprite) in spriteList)
            {
                sprite.color = Color.Lerp(startColor, new Color(1, 1, 1, 0), progress);
            }

            if (progress >= 1f)
            {
                DestroyObjectsInDict(previousDict);
            }
        });

        //Debug.Log("Finished smoothie.");
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
