using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//MMM = Mack Maker Manager ;)

public class SandwichManager : MonoBehaviour
{
    GameObject objectInHand;
    Ingredient ingredientInHand;

    public GameObject visualPrefab;
    public VisualSandwich currentVisual;
    public GameObject orderedSandwich;

    public Dictionary<Ingredient, List<GameObject>> ingredients = new();

    VisualSandwich SpawnVisual()
    {
        return Instantiate(visualPrefab).GetComponent<VisualSandwich>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentVisual = SpawnVisual();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = RayHelper.RaycastFromCamera(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out Ingredient ingredient))
                {
                    objectInHand = Instantiate(ingredient.prefab, currentVisual.placeArea.gameObject.transform, true);
                    ingredientInHand = ingredient;
                }
                else if (hit.collider.gameObject.TryGetComponent(out TrashCan trash))
                {
                    foreach (var list in ingredients.Values)
                    {
                        foreach (var obj in list)
                            Destroy(obj);
                    }
                    ingredients.Clear();
                }
            }
        }

        if (!Input.GetMouseButton(0))
        {
            if (objectInHand != null)
            {
                if (hit.collider == currentVisual.placeArea && TryPlace(objectInHand))
                {
                }
                else
                {
                    Destroy(objectInHand);
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
            objToPlace.transform.position = currentVisual.placeArea.transform.position + new Vector3(point.x, point.y, 0);
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

        currentVisual.isFinished = true;
        currentVisual.rigidBody.AddForce(new Vector2(0, 1000));

        Instantiate(orderedSandwich);
        orderedSandwich.GetComponent<OrderedSandwich>().ingredients = ingredients;

        ingredients = new Dictionary<Ingredient, List<GameObject>>();
        currentVisual = SpawnVisual();
    }
}
