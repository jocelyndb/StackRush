using UnityEngine;

public class TimePowerup : Powerup
{
    new void Start()
    {
        name = "Slow-Mo";
        Time = 7f;
        ActivatePowerup = () =>
        {
            UnityEngine.Time.timeScale = 0.5f;
            audioManager.SetSlowMo(true);
            Debug.Log("Activating slow-motion powerup");
        };

        DeactivatePowerup = () =>
        {
            UnityEngine.Time.timeScale = 1f;
            audioManager.SetSlowMo(false);
            Debug.Log("Deactivating slow-motion powerup");
        };
    }
}
