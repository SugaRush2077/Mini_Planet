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
    private int LaunchTime = 5;
    private float range = 2f;
    private Vector3 spawnCenter;
    private float spawnRadius;
    private bool isIncrease = true;
    public UltimatePlayer player;
    private Vector3 playerPos;

    private float attackRatio = .5f;
    //private int increaseSpawnTimePeriod = 5;
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
        SpawnPerSec = 1f;
        spawnRadius = landingPlanet.currentRadius * 2;
        spawnCenter = landingPlanet.transform.position;
        if (Generate)
        {
            //InvokeRepeating(nameof(Spawn), LaunchTime, (1 / SpawnPerSec));
            Invoke(nameof(Spawn), LaunchTime);
            if(isIncrease)
            {
                Invoke(nameof(updateSpawnAmount), LaunchTime);
            }
            
        }
    }

    private void FixedUpdate()
    {
        playerPos = player.transform.position;
    }

    void Spawn()
    {
        //Vector3 spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;

        Vector3 spawnPoint;
        Meteor mtr;
        // Select spawn location
        float rand = Random.value;
        if(rand <= attackRatio)
        {
            spawnPoint = spawnCenter + playerPos * range * 2f;
        }
        else
        {
            spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;
        }
        mtr = Instantiate(meteor, spawnPoint, Quaternion.identity);
        mtr.setPlayerLocation(playerPos);
        if(rand <= attackRatio)
        {
            mtr.selectPlayerAsTarget(true);
        }
        else
        {
            mtr.selectPlayerAsTarget(false);
        }

        float nextMeteor = 1;
        float frequency = Random.value;
        if(frequency < 0.7f) 
        {
            nextMeteor = Random.Range((1 / SpawnPerSec), 3);
        }
        else
        {
            nextMeteor = Random.Range(3, 5);
        }
        
        Invoke(nameof(Spawn), nextMeteor);
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
