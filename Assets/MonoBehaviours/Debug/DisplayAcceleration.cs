using UnityEngine;
using TMPro;
using System;

public class DisplayAcceleration : MonoBehaviour
{
    TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.gyro.enabled = true;
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deviceAcceleration = DeviceGyro.GetAttitude();

        // text.text = deviceAcceleration.ToString();
        // text.text = (deviceAcceleration * Vector3.forward).ToString();
        text.text = (deviceAcceleration * Vector2.up).ToString();
        // this.SetText(new String(deviceAcceleration));
    }
}
