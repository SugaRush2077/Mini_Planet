using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //public AudioSource Flying_audio;
    //public GameObject targetPlanet;
    public GameObject crater;
    public GameObject explosionEffect;
    public float explosionScale = 1.0f;

    private Vector3 towardPlanetCenter;
    private Vector3 landingPoint;
    private Quaternion craterOrientation;
    private Vector3 playerPos;
    private Vector3 flyingToward;

    public LayerMask myLayerMask;
    private float moveSpeed = 30.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //P_center = targetPlanet.transform.position;
        towardPlanetCenter = Vector3.zero;

        //selectPlayerAsTarget();
    }

    public void setPlayerLocation(Vector3 pos)
    {
        playerPos = pos;
    }

    public void selectPlayerAsTarget(bool b)
    {
        //float rand = Random.value;
        if(b)
        {
            flyingToward = playerPos - transform.position;
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

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }

    private void calculateLandPoint()
    {
        //Groundnormal = (transform.position - center).normalized;
        Ray ray = new Ray(transform.position, flyingToward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10000, myLayerMask))
        {
            
            //Debug.Log("hit: " + hit.point);
            landingPoint = hit.point;
            craterOrientation = Quaternion.LookRotation(hit.normal);
            
            //Quaternion.Normalize(ori);
        }
    }
    /*
    private void Shoot()
    {
        flyingDirection = transform.position - towardPlanetCenter;
        calculateLandPoint();

        //m_rotation = m_rotation.normalized;
        flyingDirection = flyingDirection.normalized;
        transform.rotation = landingOrientation;
    }*/

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
            
            
            Instantiate(crater, landingPoint, craterOrientation);
            
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
