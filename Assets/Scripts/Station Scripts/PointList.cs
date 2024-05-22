using UnityEditor;
using UnityEngine;

public class PointList : MonoBehaviour
{
    [SerializeField]
    public Vector2[] points;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p = points[i];
            Vector3 pos3d = transform.position + new Vector3(p.x, p.y, 0);

            float size = HandleUtility.GetHandleSize(pos3d) * 0.1f;
            Gizmos.DrawCube(pos3d, new Vector3(size, size, size));
        }
    }
}
