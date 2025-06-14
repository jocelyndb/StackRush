using System;
using System.Drawing;
using Cinemachine.Utility;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class FallingBlock : Falling
{
    public float angleSpringFactor = 4f;
    // public float maxVelocity = 5000f;
    // public float dampFactor = 0.9999f;
    // private Collider collider;
    private Rigidbody parentRB;
    private Vector3 parentDirection = Vector3.zero;
    private Quaternion targetAngle;
    // private Transform shadow;

    private new void Awake()
    {
        
        base.Awake();
        targetAngle = new Quaternion();
        // targetAngle.SetLookRotation(new Vector3(rb.transform.forward.x, 0f, rb.transform.forward.z), Vector3.up);
        rb.angularVelocity = new Vector3(0f, UnityEngine.Random.Range(2f, 3.5f) * (float)Math.Pow(-1, UnityEngine.Random.Range(0,2)), 0f);
        // shadow = transform.GetChild(0);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        if (parentRB)
        {
            FollowParent();
        }
    }

    private void FollowParent()
    {
        parentDirection = (parentRB.position - rb.position + new Vector3(0f, parentRB.transform.localScale.y, 0f));
        RightAngle();
        // rb.linearVelocity = new Vector3(Mathf.Min(parentDirection.x, maxVelocity), Mathf.Min(parentDirection.y, maxVelocity), Mathf.Min(parentDirection.z, maxVelocity));
        rb.linearVelocity = new Vector3(parentDirection.x, parentDirection.y, parentDirection.z) * GameManager.Instance.springFactor;
    }

    private void RightAngle()
    {
        rb.rotation = Quaternion.RotateTowards(rb.rotation, targetAngle, angleSpringFactor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.childCount > 1)
        {
            GameObject.Destroy(transform.GetChild(1).gameObject);
        }
        // Debug.Log(collision.collider.bounds.max)
        if (collision.rigidbody.position.y >= rb.position.y - 0.4f
            || Math.Abs(rb.position.x - collision.rigidbody.position.x) > 2 * collision.collider.bounds.extents.x / 1.5
            || Math.Abs(rb.position.z - collision.rigidbody.position.z) > 2 * collision.collider.bounds.extents.z / 1.5)
        {
            return;
        }
        // rb.linearVelocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;
        if (!parentRB)
        {
            // targetAngle.SetLookRotation(rb.transform.forward, Vector3.up)
            targetAngle.SetLookRotation(new Vector3(rb.transform.forward.x, 0f, rb.transform.forward.z), Vector3.up);
            parentRB = collision.rigidbody;
            parentRB.detectCollisions = false;
            rb.useGravity = false;
            // rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.angularDamping = 50f;
            GameManager.Instance.StackTopRB = rb;
            GameManager.Instance.StackTopCollider = GetComponent<BoxCollider>();
            Score(collision.rigidbody.position);
        }
        // collider.enabled = false;
        // collision.rigidbody.linearVelocity = Vector3.zero;
        // collision.rigidbody.angularVelocity = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Stack");
        // Debug.Log("Saw collision. Gravity is: " + (rb.useGravity ? "on" : "off"));
        // Debug.Log(rb.useGravity);
    }

    private void Score(Vector3 collidedPosition)
    {
        // Debug.Log($"Block: {rb.position}, Collided: {collidedPosition}, " +
            // $"Dot Product: {Vector3.Distance(rb.position, collidedPosition)}, Scale: {transform.localScale}, " +
            // $"Score: {GameManager.Instance.Level * (1 + (int)(transform.localScale.x / Vector3.Distance(rb.position, collidedPosition)))}");
        GameManager.Instance.Score += GameManager.Instance.Level * (1 + (int)(transform.localScale.x / Vector3.Distance(rb.position, collidedPosition) ));
    }
}
