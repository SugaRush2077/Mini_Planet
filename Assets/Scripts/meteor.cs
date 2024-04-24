using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //public AudioSource Flying_audio;
    //public GameObject targetPlanet;
    public Crater crater;
    public GameObject explosionEffect;
    public float explosionScale = 1.0f;

    private Vector3 towardPlanetCenter;
    private Vector3 landingPoint;
    private Quaternion craterOrientation;
    private Vector3 playerPos;
    private Vector3 flyingToward;
    private int currentDangerIndex;
    private float predictRadius = 2f;
    private float ATAthreshold = 0.95f; // Advanced Tracking Algorithm Threshold

    public LayerMask myLayerMask;
    private float moveSpeed = 30.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //P_center = targetPlanet.transform.position;
        towardPlanetCenter = Vector3.zero;

        //selectPlayerAsTarget();
    }


    private void randomize(int accelerate)
    {
        moveSpeed += accelerate;
        moveSpeed = Random.Range(moveSpeed - 5, moveSpeed + 5);
        transform.localScale = getRandomScale();
    }

    Vector3 getRandomScale()
    {
        float r = Random.Range(1, 4);
        return new Vector3(r, r, r);
    }

    public void getInfo(Vector3 pos, int index)
    {
        playerPos = pos;
        currentDangerIndex = index;
        randomize(currentDangerIndex);
    }

    public void selectPlayerAsTarget(bool playerIsTarget)
    {
        //float rand = Random.value;
        if(playerIsTarget)
        {
            flyingToward = playerPos - transform.position;
            Debug.Log("Original player pos:" + flyingToward);

            // Advanced Tracking Algorithm
            if (6 <= currentDangerIndex && currentDangerIndex < 12 && (Random.value < ATAthreshold))
            {
                flyingToward += (Random.onUnitSphere * predictRadius);
            }
            // Top-notch Tracking Algorithm
            else if (12 <= currentDangerIndex && (Random.value < ATAthreshold))
            {
                float rand = Random.value;
                if(rand < 0.5f) // Predict forward
                {
                    flyingToward += (Vector3.forward * predictRadius);
                }
                else if(rand < 0.7f) // Predict left
                {
                    flyingToward += (Vector3.left * predictRadius);
                }
                else if (rand < 0.9f) // Predict right
                {
                    flyingToward += (Vector3.right * predictRadius);
                }
                else // Predict backward
                {
                    flyingToward += (Vector3.back * predictRadius);
                }
            }

            Debug.Log("Predicted player pos:" + flyingToward);
        }
        else // flying toward player
        {
            flyingToward = towardPlanetCenter - transform.position;
        }

        //flyingToward = towardPlanetCenter - transform.position;
        //Debug.Log("flying toward: " + flyingToward);
        calculateLandPoint();
        
        transform.rotation = Quaternion.LookRotation(-(flyingToward));
        flyingToward = flyingToward.normalized;
        
    }

    private void calculateLandPoint()
    {
        //Groundnormal = (transform.position - center).normalized;
        Ray ray = new Ray(transform.position, flyingToward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 20000, myLayerMask))
        {
            
            //Debug.Log("hit: " + hit.point);
            landingPoint = hit.point;
            craterOrientation = Quaternion.LookRotation(hit.normal);
            
            //Quaternion.Normalize(ori);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //GetComponent<SphereCollider>().radius = 30;
        
        //collider.radius = 30;
        if (other.CompareTag("Planet") || other.CompareTag("PCG_Planet") 
            || other.CompareTag("PlanetSurface") || other.CompareTag("Player"))
        {
            //Debug.Log("Hit Planet!");
            /*
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Debug.Log("Meteor hit on: " + hitPoint);
            Quaternion normalOrientation = Quaternion.Euler(transform.position - hitPoint);
            Instantiate(crater, hitPoint, normalOrientation);*/
            Crater ctr;
            ctr = Instantiate(crater, landingPoint, craterOrientation);
            ctr.setScale(transform.localScale.x);
            
            Explode();

        }
    }


    
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //GameObject explode = Instantiate(explosionEffect, transform.position, transform.rotation);
        //explode.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        transform.position += flyingToward * moveSpeed * Time.deltaTime;
    }
}
