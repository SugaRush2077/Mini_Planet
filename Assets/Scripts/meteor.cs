using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //public GameObject targetPlanet;
    public GameObject crater;
    public GameObject explosionEffect;
    public float explosionScale = 1.0f;

    private Vector3 towardPlanetCenter;
    private Vector3 landingPoint;
    private Quaternion landingOrientation;
    private Vector3 flyingDirection;
    //private float destroy_time = 10f;
    
    private float moveSpeed = 10.0f;
    //private bool isCollide = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //P_center = targetPlanet.transform.position;
        towardPlanetCenter = Vector3.zero;
        flyingDirection = transform.position - towardPlanetCenter;
        calculateLandPoint();

        //m_rotation = m_rotation.normalized;
        flyingDirection = flyingDirection.normalized;
        transform.rotation = landingOrientation;
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }

    private void calculateLandPoint()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -flyingDirection, out hit))
        {
            Debug.Log("hit: " + hit.point);
            landingPoint = hit.point;
            landingOrientation = Quaternion.LookRotation(hit.normal);
            
            //Quaternion.Normalize(ori);
        }
    }
    private void Shoot()
    {
        flyingDirection = transform.position - towardPlanetCenter;
        calculateLandPoint();

        //m_rotation = m_rotation.normalized;
        flyingDirection = flyingDirection.normalized;
        transform.rotation = landingOrientation;
    }

    public void setTargetCenter(Vector3 vtr)
    {
        //P_center = vtr;
        //Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Planet") || other.CompareTag("PCG_Planet"))
        {
            Debug.Log("Hit Planet!");
            Instantiate(crater, landingPoint, landingOrientation);
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
        transform.position -= flyingDirection * moveSpeed * Time.deltaTime;
    }
}
