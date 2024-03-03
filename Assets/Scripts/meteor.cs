using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //public GameObject targetPlanet;
    public GameObject crater;
    public GameObject explosionEffect;
    public float explosionScale = 1.0f;

    private Vector3 P_center;
    private Vector3 landing;
    private Quaternion ori;
    private Vector3 direction;
    //private float destroy_time = 10f;
    
    private float moveSpeed = 10.0f;
    //private bool isCollide = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //P_center = targetPlanet.transform.position;

        
        
        P_center = Vector3.zero;
        direction = transform.position - P_center;
        calculateLandPoint();
        
        
        //m_rotation = m_rotation.normalized;
        direction = direction.normalized;
        transform.rotation = ori;
        
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }

    private void calculateLandPoint()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -direction, out hit))
        {
            Debug.Log("hit: " + hit.point);
            landing = hit.point;
            ori = Quaternion.LookRotation(hit.normal);
            
            //Quaternion.Normalize(ori);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Planet"))
        {
            Debug.Log("Hit Planet!");

            //Quaternion orientation = Quaternion.Euler(direction.x, direction.y, direction.z);
            //Debug.Log(orientation.ToString());
            Instantiate(crater, landing, ori);
            /*
            RaycastHit hit;
            Quaternion orientation = transform.rotation;
            if(Physics.Raycast(gameObject.transform.position, direction, out hit))
            {
                Debug.Log("Hit Ray!");
            }*/
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
        transform.position -= direction * moveSpeed * Time.deltaTime;
    }
}
