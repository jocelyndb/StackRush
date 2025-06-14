using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class TimePowerup : Powerup
{
    new void Start()
    {
        name = "Slow-Mo";
        time = 7f;
        ActivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            Time.timeScale = 0.5f;
            Debug.Log("Activating slow-motion powerup");
        };

        DeactivatePowerup = () =>
        {
            // GameManager.Instance.springFactor = 15f;
            Time.timeScale = 1f;
            Debug.Log("Deactivating slow-motion powerup");
        };
    }
}
