using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotateSpeed = 10.0f;
    public float zoomSpeed = 20.0f;

    public GameObject myGameObj;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
        Rotate();
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView += distance;
        }
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            mainCamera.transform.RotateAround(myGameObj.transform.position,
                                        Vector3.up,
                                        Input.GetAxis("Mouse X") * rotateSpeed);
            mainCamera.transform.RotateAround(myGameObj.transform.position,
                                            Vector3.right,
                                            -Input.GetAxis("Mouse Y") * rotateSpeed);
        }
    }
}
