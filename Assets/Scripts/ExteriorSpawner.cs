using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorSpawner : MonoBehaviour
{
    public GameObject centerPlanet;
    //public Planet centerPlanet;
    public Meteor meteor;

    public bool Generate = true;
    public float spawnPeriod = 1f;
    public float range = 20f;
    private Vector3 spawnCenter;
    private float spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        spawnRadius = centerPlanet.transform.localScale.x;
        //spawnRadius = centerPlanet.shapeSettings.planetRadius;
        spawnCenter = centerPlanet.transform.position;
        Debug.Log(spawnCenter);
        if (Generate)
        {
            InvokeRepeating(nameof(Spawn), 1f, spawnPeriod);
        }
        
    }

    void Spawn()
    {
        Vector3 spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius * range);
        Debug.Log(spawnPoint);
        Instantiate(meteor, spawnPoint, Quaternion.identity);
        //meteor.setTargetCenter(spawnCenter);
        //meteor.setTargetCenter(spawnCenter);
        //Debug.Log(obstacle.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
