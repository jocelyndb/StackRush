using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MagnetPowerup : Powerup
{
    new void Start()
    {
        name = "Magnet";
        time = 7f;
        ActivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            GameManager.Instance.StackTopCollider.size = new Vector3(3f, 3f, 3f);
            Debug.Log("Activating magnet powerup");
        };

        DeactivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            GameManager.Instance.StackTopCollider.size = new Vector3(1f, 1f, 1f);
            Debug.Log("Deactivating magnet powerup");
        };
    }
}
