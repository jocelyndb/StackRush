using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class MoveGyro : MonoBehaviour
{
    private Rigidbody rb;
    public InputSystem_Actions inputActions;
    private InputAction move;
    private Vector2 moveDirection;
    private Vector2 initialGyro;
    private Vector2 inputDirection;
    private Vector2 gyroDirection;
    private bool useKeyboard;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0f, 0f, 0f);
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        Input.gyro.enabled = false;
        useKeyboard = false;
    }

    private void OnEnable()
    {
        initialGyro = ClampedGyroDirection(DeviceGyro.GetAttitude());
        move = inputActions.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        // if (!useKeyboard)
        //     {
        //         useKeyboard = move.ReadValue<Vector2>().SqrMagnitude() > 0f;
        //     }
        inputDirection = move.ReadValue<Vector2>();
        if (inputDirection.magnitude > 0f)
        {
            useKeyboard = true;
        }
        // Debug.Log($"Using keyboard? {(useKeyboard ? "Yes" : "No")}");
        // TODO: make gyroDirection correct when testing with gyro
        gyroDirection = ClampedGyroDirection(DeviceGyro.GetAttitude());
        // add Dead Zone
        gyroDirection = gyroDirection.SqrMagnitude() > 0.1f ? gyroDirection : Vector2.zero;

        moveDirection = (useKeyboard || gyroDirection == initialGyro) ? inputDirection : gyroDirection;
        // print(rb.linearVelocity);
        // Vector3 deviceAcceleration = DeviceGyro.GetLinAccel();
        // Vector3 unityAcceleration = new Vector3(deviceAcceleration.x, 0f, deviceAcceleration.y);
        // rb.linearVelocity *= 0.95f;
        // rb.linearVelocity += unityAcceleration * Time.deltaTime * -100f;
        // transform.rotation = DeviceGyro.GetAttitude();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveDirection.x * GameManager.Instance.moveSpeed, 0f, moveDirection.y * GameManager.Instance.moveSpeed), ForceMode.Acceleration);
        // transform.position += new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    private Vector2 ClampedGyroDirection(Quaternion input)
    {
        Vector3 gyroVector = input * Vector2.up;
        Debug.Log(new Vector2(gyroVector.z * 5f, (gyroVector.y - .725f) * -4.5f));
        return new Vector2(Math.Clamp(gyroVector.z * 5f, -1f, 1f), Math.Clamp((gyroVector.y - .725f) * -4.5f, -1f, 1f));
        // return DeviceGyro.GetAttitude() * Vector3.forward;
    }
}
