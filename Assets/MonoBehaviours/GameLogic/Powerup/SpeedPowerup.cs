using UnityEngine;

public class SpeedPowerup : Powerup
{
    new void Start()
    {
        name = "Speed";
        Time = 10f;
        ActivatePowerup = () =>
        {
            GameManager.Instance.moveSpeed = 2f * GameManager.Instance.BaseMoveSpeed;
            Debug.Log("Activating speed powerup");
        };

        DeactivatePowerup = () =>
        {
            GameManager.Instance.moveSpeed = GameManager.Instance.BaseMoveSpeed;
            Debug.Log("Deactivating speed powerup");
        };
    }
}
