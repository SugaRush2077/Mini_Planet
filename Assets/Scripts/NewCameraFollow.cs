using UnityEngine;

public class NewCameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    public UltimatePlayer target;
    private float smoothTime;
    public Vector3 _currentVelocity;
    private float offsetValue = 30f;

    private void Awake()
    {
        _offset = transform.position - target.transform.position;
        transform.LookAt(target.transform);
    }
    /*
    private void LateUpdate()
    {
        _offset = target.Groundnormal * offsetValue;
        Vector3 targetPosition = target.transform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        Debug.Log(target.Groundnormal);
        //Quaternion toRotation = Quaternion.FromToRotation(transform.up, target.Groundnormal) * transform.rotation;
        //transform.rotation = toRotation;

        //transform.LookAt(target.transform);
    }*/

    
}
