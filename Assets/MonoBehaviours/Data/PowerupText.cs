using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        string listedPowerups = "";
        foreach (Powerup powerup in GameManager.Instance.powerups)
        {
            listedPowerups += powerup.name + "\n";
        }
        text.text = "Powerups:\n" + listedPowerups;
    }
}
