using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class UltimatePlayer : MonoBehaviour
{
    public GameObject Planet;
    //public GameObject PlayerPlaceholder;
    private float DefaultMovingSpeed = 7f;
    private float DefaultBoostSpeed = 16f; // 11f
    private float speed;
    //private float flyMomentum = 20f;
    //private float landingMomentum = 7f;
    private float rotateDegree = 90f;

    float gravityMagnitude = 100;
    bool OnGround = false;

    float distanceToGround;
    bool successLanding = false;
    bool isFlying = false;
    bool isBoosting = false;
    bool isGoingOutside = false;

    Vector3 GroundCenterNormal = Vector3.zero;
    Vector3 absNormalUp;
    Vector3 startPos = new Vector3(0, 80, 0);
    private Rigidbody rb;

    public delegate void CCompletedEventHandler();
    public static event CCompletedEventHandler whenPlayerDead;

    public AudioClip explosion_audio;
    private Light light;
    

    void Awake()
    {
        initialize();
    }
    public void initialize()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        speed = DefaultMovingSpeed;
        OnGround = false;
        successLanding = false;
        isFlying = false;
        isBoosting = false;
        light = GetComponent<Light>();
        light.color = Color.red;
        isGoingOutside = false;
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
            if (!isBoosting)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(0, rotateDegree * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(0, -rotateDegree * Time.deltaTime, 0);
                }
            }

            //Debug.Log(transform.up);

            //Fly
            /*
            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Fly");
                if (getFlying())
                {
                    float y = flyMomentum * Time.deltaTime;
                    transform.Translate(0, y, 0);
                }
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                if (isBoosting)
                {
                    // turn off boosting
                    speed = DefaultMovingSpeed;
                    light.color = Color.red;
                    Debug.Log("Normal Move!");
                    isBoosting = false;
                }
                else
                {
                    // turn on boosting
                    speed = DefaultBoostSpeed;
                    light.color = Color.blue;
                    Debug.Log("Boosting!");
                    isBoosting = true;
                }
            }
            /*
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
            */
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
        /*
        if(isFlying)
        {
            speed = DefaultBoostSpeed;
        }
        else
        {
            speed = DefaultMovingSpeed;
        }*/
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
        if(getOnGround())
        {
            //Debug.Log("AlignAbs");
            toDir = absNormalUp;
        }
        else
        {
            //Debug.Log("AlignGround");
            toDir = GroundCenterNormal;
        }
        toDir = GroundCenterNormal;
        transform.rotation = Quaternion.FromToRotation(transform.up, toDir) * transform.rotation;
    }

    void addGravity() // if player is not stand on ground, force it to the ground (Add Gravity)
    {
        if (!getOnGround())
        {
            if (isGoingOutside)
            {
                rb.AddForce(-absNormalUp * gravityMagnitude * 10);
            }
            else
            {
                rb.AddForce(-absNormalUp * gravityMagnitude);
            }
            
        }
        
    }

    private void isOnGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 20000))
        {
            distanceToGround = hit.distance;
            GroundCenterNormal = hit.normal;

            if (distanceToGround <= 1f)
            {
                setIsOnGround(true);
                isGoingOutside = true;
            }
            else
            {
                setIsOnGround(false);
                if(distanceToGround > 100f)
                {
                    isGoingOutside = true;
                }
            }
        }
        else
        {
            setIsOnGround(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Obstacle"))
        {
            OnCCompletedPlayerDead();
            SoundFXManager.instance.PlaySoundFXClip(explosion_audio, transform, .7f);
            GameManager.Instance.GameOver();
        }
    }
/*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnCCompletedPlayerDead();
            SoundFXManager.instance.PlaySoundFXClip(explosion_audio, transform, .7f);
            GameManager.Instance.GameOver();
        }
    }*/
    /*
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("BURN!");
    }*/
}
