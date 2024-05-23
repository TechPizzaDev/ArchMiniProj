using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    private List<OrderDesc> orders = new();

    StringBuilder noteBuilder = new();
    private bool needsRedraw;

    public GameObject orderNoteRoot;
    public GameObject orderNotePrefab;
    public int noteSpacing = 260;

    void Start()
    {
        RedrawNotes(noteBuilder, 0);
    }

    void Update()
    {
        if (needsRedraw)
        {
            RedrawNotes(noteBuilder, 3);

            needsRedraw = false;
        }
    }

    private void RedrawNotes(StringBuilder builder, int maxCount)
    {
        for (int i = 0; i < orderNoteRoot.transform.childCount; i++)
        {
            Destroy(orderNoteRoot.transform.GetChild(i).gameObject);
        }

        int count = 0;

        foreach (var order in orders)
        {
            if (count >= maxCount)
            {
                break;
            }

            GameObject instance = Instantiate(orderNotePrefab, orderNoteRoot.transform);
            instance.transform.localPosition -= new Vector3(0, noteSpacing * count, 0);

            OrderNote note = instance.GetComponent<OrderNote>();

            note.label.text = "Order #" + order.id;

            builder.Clear();
            BuildNoteContents(order, builder);
            note.contents.text = builder.ToString();

            count++;
        }
    }

    private void BuildNoteContents(OrderDesc order, StringBuilder builder)
    {
        builder.Append("<b>").Append(order.ingredients.First().Key.productName).AppendLine("</b>: ");

        bool hasItems = false;

        foreach ((Ingredient ingredient, int count) in order.ingredients)
        {
            if (hasItems)
            {
                builder.AppendLine(", ");
            }

            builder.Append(count).Append("x ");
            builder.Append(ingredient.ingredientName);

            hasItems = true;
        }
    }

    public void Enqueue(OrderDesc order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        orders.Add(order);
        needsRedraw = true;
    }

    public void Remove(OrderDesc order)
    {
        if (orders.Remove(order))
        {
            needsRedraw = true;
        }
    }
}
