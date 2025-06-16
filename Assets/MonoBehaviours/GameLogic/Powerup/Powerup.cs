using System;
using System.Collections;
using UnityEngine;

public class Powerup : Falling
{
    public Action ActivatePowerup;
    public Action DeactivatePowerup;
    public String Name = "Powerup";
    protected float Time = 10f;

    protected void Start()
    {
        ActivatePowerup = () =>
        {
            Debug.Log("Activating base powerup");
        };

        DeactivatePowerup = () =>
        {
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
        audioManager.PlaySFX(audioManager.powerup);
        rb.detectCollisions = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Starting powerup flow");
        StartCoroutine(PowerupFlow());
    }

    private IEnumerator PowerupFlow()
    {
        GameManager.Instance.powerups.Add(this);

        ActivatePowerup();
        Debug.Log("Activated powerup");

        yield return new WaitForSeconds(Time);

        DeactivatePowerup();
        Debug.Log("Deactivated powerup");
        
        GameManager.DeletePowerUp(this);
        Destroy(gameObject);
    }
}
