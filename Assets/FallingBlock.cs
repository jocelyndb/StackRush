using System;
using Cinemachine.Utility;
using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class FallingBlock : MonoBehaviour
{
    public float springFactor = 8f;
    public float maxVelocity = 5f;
    // public float dampFactor = 0.9999f;
    private Rigidbody rb;
    // private Collider collider;
    private Rigidbody parentRB;
    private Vector3 parentDirection = Vector3.zero;
    private Quaternion targetAngle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // collider = GetComponent<Collider>();
        targetAngle = new Quaternion();
        targetAngle.SetLookRotation(rb.transform.forward, rb.transform.up);
    }

    private void FixedUpdate()
    {
        if (parentRB)
        {
            FollowParent();
        }
        
        if (rb.position.y < -2)
        {
            OnOutOfBounds();
        }
    }

    private void FollowParent()
    {
        parentDirection = (parentRB.position - rb.position + new Vector3(0f, parentRB.transform.localScale.y, 0f)) * springFactor;
        RightAngle();
        rb.linearVelocity = new Vector3(Mathf.Min(parentDirection.x, maxVelocity), Mathf.Min(parentDirection.y, maxVelocity), Mathf.Min(parentDirection.z, maxVelocity));
    }

    private void RightAngle()
    {
        rb.rotation = Quaternion.RotateTowards(rb.rotation, targetAngle, 0.5f);
    }

    private void OnOutOfBounds()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.position.y >= rb.position.y - 0.4f
            || Math.Abs(rb.position.x - collision.rigidbody.position.x) > transform.localScale.x / 1.5
            || Math.Abs(rb.position.z - collision.rigidbody.position.z) > transform.localScale.z / 1.5)
        {
            return;
        }
        // rb.linearVelocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;
        if (!parentRB)
        {
            parentRB = collision.rigidbody;
            parentRB.detectCollisions = false;
            rb.useGravity = false;
            // rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.angularDamping = 50f;
        }
        // collider.enabled = false;
        // collision.rigidbody.linearVelocity = Vector3.zero;
        // collision.rigidbody.angularVelocity = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Stack");
        // Debug.Log("Saw collision. Gravity is: " + (rb.useGravity ? "on" : "off"));
        // Debug.Log(rb.useGravity);
    }
}
