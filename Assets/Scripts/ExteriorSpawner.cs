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
    public int LaunchTime;
    private float range = 2f;
    private Vector3 spawnCenter;
    private float spawnRadius;

    private int increaseSpawnTimePeriod = 5;
    //private float time = 0;

    // Start is called before the first frame update
    
    void Start()
    {
        
        //Debug.Log(spawnRadius);
        //Launch();
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Launch()
    {
        SpawnPerSec = 1;
        spawnRadius = landingPlanet.currentRadius * 2;
        spawnCenter = landingPlanet.transform.position;
        if (Generate)
        {
            //InvokeRepeating(nameof(Spawn), LaunchTime, (1 / SpawnPerSec));
            Invoke(nameof(Spawn), LaunchTime);
            Invoke(nameof(updateSpawnAmount), LaunchTime);
        }
    }
    
    void Spawn()
    {
        Vector3 spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;
        //Debug.Log(spawnPoint);
        Instantiate(meteor, spawnPoint, Quaternion.identity);
        Invoke(nameof(Spawn), (1 / SpawnPerSec));
        Debug.Log("Current SpawnPerSec: " + SpawnPerSec);
    }

    void updateSpawnAmount()
    {
        if (SpawnPerSec < 10)
        {
            SpawnPerSec++;
        }
        
        Invoke(nameof(updateSpawnAmount), 5);
    }

}
