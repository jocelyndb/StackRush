using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float cameraLerpFactor = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    // void Update()
    // {
    //     // transform.position = new Vector3(transform.position.x, GameManager.Instance.StackTop.position.y, transform.position.z);
    // }

    void LateUpdate()
    {
        // Vector3 stackTopCameraPosition = new Vector3(transform.position.x, GameManager.Instance.StackTop.position.y + 10f, transform.position.z);
        Vector3 stackTopCameraPosition = new Vector3(GameManager.Instance.StackTop.position.x, GameManager.Instance.StackTop.position.y + 10f, GameManager.Instance.StackTop.position.z - 20f);
        if (stackTopCameraPosition != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, stackTopCameraPosition, Time.deltaTime * cameraLerpFactor);
        }
    }
}
