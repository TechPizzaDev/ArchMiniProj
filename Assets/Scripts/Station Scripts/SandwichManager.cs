using System.Collections.Generic;
using UnityEngine;

//MMM = Mack Maker Manager ;)

public class SandwichManager : MonoBehaviour
{
    GameObject objectInHand;
    Ingredient ingredientInHand;

    public Collider2D placeArea;

    public Dictionary<Ingredient, int> placedIngredients = new();

    // Start is called before the first frame update
    void Start()
    {

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
                    objectInHand = Instantiate(ingredient.prefab, hit.collider.gameObject.transform, true);
                    ingredientInHand = ingredient;
                }
            }
        }

        if (!Input.GetMouseButton(0))
        {
            if (objectInHand != null)
            {
                if (hit.collider == placeArea && TryPlace(objectInHand))
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

        int ingdCount = placedIngredients.GetValueOrDefault(ingredientInHand, 0);
        if (ingdCount < pointList.points.Length)
        {
            placedIngredients[ingredientInHand] = ingdCount + 1;

            Vector2 point = pointList.points[ingdCount % pointList.points.Length];
            objToPlace.transform.position = placeArea.transform.position + new Vector3(point.x, point.y, 0);
            return true;
        }
        return false;
    }
}
