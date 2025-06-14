using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Powerup : Falling
{
    public Action ActivatePowerup;
    public Action DeactivatePowerup;
    public String Name = "Powerup";
    protected float time = 10f;

    protected void Start()
    {
        // Magnet powerup (for testing)
        ActivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            GameManager.Instance.StackTopCollider.size = new Vector3(5f, 5f, 5f);
            Debug.Log("Activating base powerup");
        };

        DeactivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            GameManager.Instance.StackTopCollider.size = new Vector3(1f, 1f, 1f);
            Debug.Log("Deactivating base powerup");
        };
    }

    private new void Awake()
    {
        base.Awake();
        rb.angularVelocity = new Vector3(0f, UnityEngine.Random.Range(2f, 3.5f) * (float)Math.Pow(-1, UnityEngine.Random.Range(0,2)), 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.detectCollisions = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        Debug.Log($"Now there are {GameManager.Instance.powerups.Count()} powerups in the GameManager");
        Debug.Log("Starting powerup flow");
        StartCoroutine(PowerupFlow());
        // ActivatePowerup();
        // Coroutine()
    }

    private IEnumerator PowerupFlow()
    {
        GameManager.Instance.powerups.Add(this);
        ActivatePowerup();
        Debug.Log("Activated powerup");
        yield return new WaitForSeconds(time);
        DeactivatePowerup();
        Debug.Log("Deactivated powerup");
        GameManager.DeletePowerUp(this);
        // GameManager.Instance.powerups.Remove(this);
        Destroy(gameObject);
    }
}
