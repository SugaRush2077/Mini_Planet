using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    //public GameObject targetPlanet;
    public GameObject crater;

    private Vector3 P_center;
    private Vector3 landing;
    private Quaternion ori;
    private Vector3 direction;
    private float speed = 10.0f;
    private bool isCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        //P_center = targetPlanet.transform.position;

        
        
        P_center = Vector3.zero;
        direction = transform.position - P_center;
        calculateLandPoint();
        //Debug.Log(direction);
        //ori = Quaternion.Euler(direction.x, direction.y, direction.z);
        //Debug.Log(ori);
        direction = direction.normalized;
        
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
        if(other.tag == "Planet")
        {
            Debug.Log("Hit Planet!");
            isCollide = true;

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
            Destroy(gameObject);

        }
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        transform.position -= direction * speed * Time.deltaTime;
    }
}
