using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buggy : MonoBehaviour
{
    public Transform gravityTarget;
    public float power = 1500f;
    public float torque = 500f;
    public float gravity = 9.81f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ProcessInput();
        ProcessGravity();
    }

    private void ProcessInput()
    {
        // Accelerate
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt * power);
        rb.AddRelativeForce(force);

        // Turn
        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(0f, hz * torque, 0f);
        rb.AddRelativeTorque(rforce);
    }

    private void ProcessGravity() 
    {
        Vector3 diff = transform.position - gravityTarget.position; // position diff between our object and planet
        rb.AddForce(diff - diff.normalized * gravity * (rb.mass));
        Debug.DrawRay(transform.position, diff.normalized, Color.red);

        if(autoOrient) {
            AutoOrient(-diff);
        }

    }   

    private void AutoOrient(Vector3 down)
    {
        Quaternion orientDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientDirection, autoOrientSpeed * Time.deltaTime);
    }
}
