using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorSpawner : MonoBehaviour
{
    public Planet landingPlanet;
    //public Planet centerPlanet;
    public Meteor meteor;

    public bool Generate = true;
    public float SpawnPerSec;
    private float range = 3f;
    private Vector3 spawnCenter;
    private float spawnRadius;

    
    // Start is called before the first frame update
    void Start()
    {
        
        spawnRadius = landingPlanet.currentRadius * 2;
        spawnCenter = landingPlanet.transform.position;
        //Debug.Log(spawnRadius);
        Launch();
    }

    public void Launch()
    {
        if (Generate)
        {
            InvokeRepeating(nameof(Spawn), 5f, (1 / SpawnPerSec));
        }
    }

    void Spawn()
    {
        Vector3 spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;
        //Debug.Log(spawnPoint);
        Instantiate(meteor, spawnPoint, Quaternion.identity);
    }
    
}
