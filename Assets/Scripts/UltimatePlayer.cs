using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePlayer : MonoBehaviour
{
    public GameObject Planet;
    //public GameObject PlayerPlaceholder;

    public float speed = 4;
    public float JumpHeight = 1.5f;
    private float rotateDegree = 90f;

    float gravityMagnitude = 100;
    bool OnGround = false;

    float distanceToGround;
    bool firstTouch = false;
    //public Vector3 Groundnormal;
    //public Vector3 Forward = new Vector3(1, 0, 0);

    private Rigidbody rb;
    Vector3 absNormalUp;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!firstTouch && OnGround)
        {
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            firstTouch = true;
        }

        if (firstTouch)
        { 
            //MOVEMENT
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Translate(x, 0, z);

            //Local Rotation
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, rotateDegree * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -rotateDegree * Time.deltaTime, 0);
            }
            //Debug.Log(transform.up);
            
            
        }
        absNormalUp = (transform.position - Planet.transform.position).normalized;
        //Debug.Log("Normal Up vec: " + absNormalUp);
        // Detect Ground Direction and adjust player belly to the ground 

        //Debug.Log("Current pos: " + transform.position);
        //Debug.Log("up vec: " + transform.up);
        RaycastHit hit = new RaycastHit();
        Vector3 Groundnormal = Vector3.zero;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1000))
        {
            
            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 2f)
            {
                //Debug.Log("OnGround");
                OnGround = true;
            }
            else
            {
                //Debug.Log("OffGround");
                OnGround = false;
            }
        }
        //GRAVITY and ROTATION
        Vector3 gravDirection = -absNormalUp;

        // if player is not stand on ground, force it to the ground (Add Gravity)
        if (OnGround == false)
        {
            rb.AddForce(gravDirection * gravityMagnitude);
        }

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            rb.AddForce(absNormalUp * 40000 * JumpHeight * Time.deltaTime);
        }
    }
}
