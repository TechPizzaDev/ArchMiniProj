using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddToList : MonoBehaviour
{
    public SandwichManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool ingredient = 
        manager.smoothieIngredients.Add(collision.gameObject.transform.parent.gameObject);
       
        if (ingredient)
        {
            Debug.Log("Added once.");

        }
    }
}
