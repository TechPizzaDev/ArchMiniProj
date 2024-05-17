using UnityEngine;

public class TriggerAddToList : MonoBehaviour
{
    public SandwichManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.AddIngredientObject(collision.transform.parent.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        manager.RemoveIngredientObject(collision.transform.parent.gameObject);
    }
}
