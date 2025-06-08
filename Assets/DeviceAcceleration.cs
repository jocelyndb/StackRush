using UnityEngine;

public class DeviceGyro : MonoBehaviour
{
    public static bool gyroInitialized = false;

    public static bool HasGyroScope
    {
        get
        {
            return SystemInfo.supportsGyroscope;
        }
    }

    public static Vector3 GetLinAccel()
    {
        if (!gyroInitialized)
        {
            InitGyro();
        }

        return HasGyroScope
            ? ReadGyroscopeAcceleration()
            : new Vector3(0f, 0f, 0f);
    }

    public static Quaternion GetAttitude()
    {
        if (!gyroInitialized)
        {
            InitGyro();
        }

        return HasGyroScope
            ? ReadGyroscopeAttitude()
            : Quaternion.identity;
    }

    private static void InitGyro()
    {
        if (HasGyroScope)
        {
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.001f;
        }
        gyroInitialized = true;
    }

    private static Vector3 ReadGyroscopeAcceleration()
    {
        return Input.gyro.userAcceleration;
    }
    
    private static Quaternion ReadGyroscopeAttitude()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
