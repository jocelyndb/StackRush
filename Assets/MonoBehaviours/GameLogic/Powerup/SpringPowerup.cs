using UnityEngine;

public class SpringPowerup : Powerup
{
    new void Start()
    {
        name = "Cheese Pull";
        Time = 10f;
        ActivatePowerup = () =>
        {
            GameManager.Instance.springFactor = 3f * GameManager.Instance.BaseSpringFactor;
            Debug.Log("Activating spring powerup");
        };

        DeactivatePowerup = () =>
        {
            GameManager.Instance.springFactor = GameManager.Instance.BaseSpringFactor;
            Debug.Log("Deactivating spring powerup");
        };
    }
}
