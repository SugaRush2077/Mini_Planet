using UnityEngine;

public class BrackeysCameraFollow : MonoBehaviour
{
    public UltimatePlayer target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 Groundnormal;
    /*
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + target.Groundnormal * 20f;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 20) && hit.collider.tag == "Core")
        {
            Groundnormal = hit.normal;
        }
        transform.LookAt(target.transform, Groundnormal);
    }*/
}
