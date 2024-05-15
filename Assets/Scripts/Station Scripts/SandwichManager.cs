using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//MMM = Mack Maker Manager ;)

public class SandwichManager : MonoBehaviour
{
    GameObject objectInHand;
    Ingredient ingredientInHand;

    public GameObject visualPrefab;
    public VisualSandwich currentSandwich;
    public VisualBlender currentSmoothie;
    public GameObject orderedSandwich;

    public Dictionary<Ingredient, List<GameObject>> ingredients = new();
    public HashSet<GameObject> smoothieIngredients = new();

    VisualSandwich SpawnVisual()
    {
        return Instantiate(visualPrefab).GetComponent<VisualSandwich>();
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

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out IngredientSandwich ingredientSandwich))
                {
                    objectInHand = Instantiate(ingredientSandwich.prefab, currentSandwich.placeArea.gameObject.transform, true);
                    ingredientInHand = ingredientSandwich;
                }
                else if (hit.collider.gameObject.TryGetComponent(out IngredientSmoothie ingredientSmoothie))
                {
                    objectInHand = Instantiate(ingredientSmoothie.prefab, currentSmoothie.placeArea.gameObject.transform, true);
                    objectInHand.GetComponent<Rigidbody2D>().isKinematic = true;
                    ingredientInHand = ingredientSmoothie;
                }
                else if (hit.collider.gameObject.TryGetComponent(out TrashCan trash))
                {
                    foreach (var obj in smoothieIngredients)
                    {
                        Destroy(obj);
                    }
                    foreach (var list in ingredients.Values)
                    {
                        foreach (var obj in list)
                            Destroy(obj);
                    }
                    ingredients.Clear();
                    smoothieIngredients.Clear();
                }
            }
        }

        if (!Input.GetMouseButton(0))
        {
            if (objectInHand != null)
            {
                if (ingredientInHand is IngredientSandwich)
                {
                    if (hit.collider == currentSandwich.placeArea && TryPlace(objectInHand))
                    {

                    }
                    else
                    {
                        Destroy(objectInHand);
                    }
                }
                else if(ingredientInHand is IngredientSmoothie)
                {
                    objectInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                }
                objectInHand = null;
                ingredientInHand = null;
            }
        }

        if (objectInHand != null)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -0.1f;
            objectInHand.transform.position = pos;
        }
    }

    bool TryPlace(GameObject objToPlace)
    {
        if (!objToPlace.TryGetComponent(out PointList pointList))
        {
            return true;
        }

        if (!ingredients.TryGetValue(ingredientInHand, out var ingdList))
        {
            ingdList = new List<GameObject>();
            ingredients.Add(ingredientInHand, ingdList);
        }

        if (ingdList.Count < pointList.points.Length)
        {
            ingdList.Add(objToPlace);

            Vector2 point = pointList.points[(ingdList.Count - 1) % pointList.points.Length];
            objToPlace.transform.position = currentSandwich.placeArea.transform.position + new Vector3(point.x, point.y, 0);
            return true;
        }
        return false;
    }

    public void FinishedSandwich()
    {
        if (ingredients.Count == 0)
        {
            return;
        }

        currentSandwich.isFinished = true;
        currentSandwich.rigidBody.AddForce(new Vector2(0, 1000));

        Instantiate(orderedSandwich);
        orderedSandwich.GetComponent<OrderedSandwich>().ingredients = ingredients;

        ingredients = new Dictionary<Ingredient, List<GameObject>>();
        currentSandwich = SpawnVisual();
    }
}
