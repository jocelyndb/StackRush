using UnityEngine;

public class Falling : MonoBehaviour
{
    // public float dampFactor = 0.9999f;
    protected Rigidbody rb;
    // private Collider collider;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // collider = GetComponent<Collider>();
    }

    protected void FixedUpdate()
    {
        if (rb.position.y < -2)
        {
            OnOutOfBounds();
        }
    }

    protected void OnOutOfBounds()
    {
        Destroy(gameObject);
    }
}
