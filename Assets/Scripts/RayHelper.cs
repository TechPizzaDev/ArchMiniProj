using UnityEngine;

#nullable enable

public static class RayHelper
{
    public static RaycastHit2D RaycastFromCamera(Vector3 screenPosition)
    {
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screenPosition), Vector2.zero);
    }
}