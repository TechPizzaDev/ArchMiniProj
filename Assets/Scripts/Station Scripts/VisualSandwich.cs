using UnityEngine;

public class VisualSandwich : MonoBehaviour
{
    float timer;

    public float lifeTime = 2;
    public bool isFinished;

    public Collider2D placeArea;
    public Rigidbody2D rigidBody;

    private void Start()
    {
        placeArea = GetComponentInChildren<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isFinished)
        {
            return;
        }

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
