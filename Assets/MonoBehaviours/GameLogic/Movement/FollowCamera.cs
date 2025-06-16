using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float CameraLerpFactor = 10f;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // transform.position = new Vector3(transform.position.x, GameManager.Instance.StackTopRB.position.y, transform.position.z);
    // }

    void LateUpdate()
    {
        // Vector3 stackTopCameraPosition = new Vector3(transform.position.x, GameManager.Instance.StackTopRB.position.y + 10f, transform.position.z);
        Vector3 stackTopCameraPosition = new Vector3(GameManager.Instance.StackTopRB.position.x, GameManager.Instance.StackTopRB.position.y + 10f, GameManager.Instance.StackTopRB.position.z - 20f);
        if (stackTopCameraPosition != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, stackTopCameraPosition, Time.deltaTime * CameraLerpFactor);
        }
        cam.fieldOfView = 60 + transform.position.y;
    }
}
