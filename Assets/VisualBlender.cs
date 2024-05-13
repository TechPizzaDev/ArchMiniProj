using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBlender : MonoBehaviour
{
    public Collider2D placeArea;

    private void Start()
    {
        placeArea = GetComponentInChildren<Collider2D>();
    }
}
