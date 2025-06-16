using UnityEngine;

public class MagnetPowerup : Powerup
{
    new void Start()
    {
        name = "Magnet";
        Time = 7f;
        ActivatePowerup = () =>
        {
            GameManager.Instance.StackTopCollider.size = new Vector3(3f, 3f, 3f);
            Debug.Log("Activating magnet powerup");
        };

        DeactivatePowerup = () =>
        {
            GameManager.Instance.StackTopCollider.size = new Vector3(1f, 1f, 1f);
            Debug.Log("Deactivating magnet powerup");
        };
    }
}
