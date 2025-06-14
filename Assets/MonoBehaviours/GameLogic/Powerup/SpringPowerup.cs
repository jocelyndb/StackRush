using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SpringPowerup : Powerup
{
    new void Start()
    {
        name = "Cheese Pull";
        time = 10f;
        ActivatePowerup = () =>
        {
            GameManager.Instance.springFactor = 3f * GameManager.Instance.baseSpringFactor;
            Debug.Log("Activating spring powerup");
        };

        DeactivatePowerup = () =>
        {
            GameManager.Instance.springFactor = GameManager.Instance.baseSpringFactor;
            Debug.Log("Deactivating spring powerup");
        };
    }
}
