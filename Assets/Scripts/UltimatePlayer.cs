using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UltimatePlayer : MonoBehaviour
{
    public GameObject Planet;
    //public GameObject PlayerPlaceholder;
    private float DefaultMovingSpeed = 5f;
    private float DefaultFlyingSpeed = 11f;
    private float speed;
    private float flyMomentum = 20f;
    //private float landingMomentum = 7f;
    private float rotateDegree = 90f;

    float gravityMagnitude = 100;
    bool OnGround = false;

    float distanceToGround;
    bool successLanding = false;
    bool isFlying = false;

    Vector3 Groundnormal = Vector3.zero;
    Vector3 absNormalUp;
    Vector3 startPos = Vector3.zero;
    private Rigidbody rb;

    public delegate void CCompletedEventHandler();
    public static event CCompletedEventHandler whenPlayerDead;

    void Start()
    {
        //transform.position = startPos;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        speed = DefaultMovingSpeed;
    }

    private void Update()
    {
        if (successLanding)
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

            //Fly
            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Fly");
                if (getFlying())
                {
                    float y = flyMomentum * Time.deltaTime;
                    transform.Translate(0, y, 0);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!getFlying())
                {
                    setIsFlying(true);
                    Debug.Log("Switch to fly!");
                }
                else
                {
                    setIsFlying(false);
                    Debug.Log("Switch to move!");
                }
                

            }
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {
                setIsFlying(true);
                Debug.Log("pushed");
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                setIsFlying(false);
                Debug.Log("Up");
            }*/
        }
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        if (!successLanding && OnGround)    // When landing, freeze and start the game
        {
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            successLanding = true;
        }

        isOnGround();
        calculateAbsNormalUp();
        AlignTopVec();
        addGravity();

    }

    private void OnCCompletedPlayerDead()
    {
        whenPlayerDead?.Invoke();
    }

    public void setStartPos(float r)
    {
        startPos.y += (r + 10);
    }

    void calculateAbsNormalUp()
    {
        absNormalUp = (transform.position - Planet.transform.position).normalized;
    }

    void setIsFlying(bool isFly)
    {
        isFlying = isFly;
        if(isFlying)
        {
            speed = DefaultFlyingSpeed;
        }
        else
        {
            speed = DefaultMovingSpeed;
        }
    }

    void setIsOnGround(bool isGround)
    {
        OnGround = isGround;
        
    }
    bool getOnGround() { return OnGround; }
    bool getFlying() { return isFlying; }

    void AlignTopVec() // Detect Ground Direction and adjust player's belly snip to ground
    {
        Vector3 toDir;
        if(getFlying())
        {
            //Debug.Log("AlignAbs");
            toDir = absNormalUp;
        }
        else
        {
            //Debug.Log("AlignGround");
            toDir = Groundnormal;
        }
        transform.rotation = Quaternion.FromToRotation(transform.up, toDir) * transform.rotation;
    }

    void addGravity() // if player is not stand on ground, force it to the ground (Add Gravity)
    {
        if (!getOnGround())
        {
            rb.AddForce(-absNormalUp * gravityMagnitude);
        }
    }

    private void isOnGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1000))
        {
            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 1f)
            {
                setIsOnGround(true);
            }
            else
            {
                setIsOnGround(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnCCompletedPlayerDead();
            GameManager.Instance.GameOver();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("BURN!");
    }
}
