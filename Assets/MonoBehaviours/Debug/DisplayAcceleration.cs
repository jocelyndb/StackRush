using UnityEngine;
using TMPro;
using System;

public class DisplayAcceleration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deviceAcceleration = DeviceGyro.GetLinAccel();

        print(deviceAcceleration);
        // this.SetText(new String(deviceAcceleration));
    }
}
