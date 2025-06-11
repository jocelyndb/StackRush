using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    new void Start()
    {
        name = "Speed";
        time = 10f;
        ActivatePowerup = () =>
        {
            GameManager.Instance.moveSpeed = 2f * GameManager.Instance.baseMoveSpeed;
            Debug.Log("Activating speed powerup");
        };

        DeactivatePowerup = () =>
        {
            GameManager.Instance.moveSpeed = GameManager.Instance.baseMoveSpeed;
            Debug.Log("Deactivating speed powerup");
        };
    }
}
