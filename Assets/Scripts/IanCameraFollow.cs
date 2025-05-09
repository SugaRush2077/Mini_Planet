using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IanCameraFollow : MonoBehaviour
{
    public UltimatePlayer target;
    public Vector3 center = new Vector3(0,0,0);
    public float smoothSpeed = 10f; //.125f
    public float offset = 20f;
    public float CameraRotateSpeed = 3.5f;
    public Camera cam;

    private float sensitivity = 17f;
    float minFov = 45;
    float maxFov = 110;

    private Vector3 Groundnormal;

    private void LateUpdate()
    {
        Groundnormal = (transform.position - center).normalized;
        //Debug.Log(Groundnormal);
        
        Vector3 desiredPosition = target.transform.position + Groundnormal * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = desiredPosition;
        transform.position = smoothedPosition;


        // Rotate Camera

        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * CameraRotateSpeed);
            transform.RotateAround(target.transform.position, transform.right, -Input.GetAxis("Mouse Y") * CameraRotateSpeed);
        }
        else
        {
            transform.LookAt(target.transform, Groundnormal);
        }

        //ZOOM
        
        float fov = cam.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.fieldOfView = fov;

    }
}
