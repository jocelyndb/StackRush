using UnityEngine;

public class Falling : MonoBehaviour
{
    // public float dampFactor = 0.9999f;
    protected Rigidbody rb;
    // private Collider collider;
    protected AudioManager audioManager;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // collider = GetComponent<Collider>();
    }

    protected void FixedUpdate()
    {
        if (rb.position.y < -2)
        {
            OnOutOfBounds();
        }
    }

    virtual protected void OnOutOfBounds()
    {
        Destroy(gameObject);
    }
}
