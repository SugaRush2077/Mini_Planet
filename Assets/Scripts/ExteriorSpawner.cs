using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorSpawner : MonoBehaviour
{
    public Planet landingPlanet;
    public Meteor meteor;
    public UltimatePlayer player;
    private Vector3 playerPos;

    private Vector3 spawnCenter;
    private float range = 2f;
    private float spawnRadius;

    private bool isIncrease = true;
    public bool Generate = true;
    private int LaunchTime = 5;

    const float escalatePeriod = 10f; // every n seconds update the index
    private float SpawnPerSec;
    private float attackInterval;
    private int dangerIndex;
    private float attackToPlayerRatio;

    void Start()
    {
        //Debug.Log(spawnRadius);
        //Launch();
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Initialize()
    {
        dangerIndex = 0;
        SpawnPerSec = 1f;
        attackToPlayerRatio = .5f;
        attackInterval = .5f;

        spawnRadius = landingPlanet.currentRadius * 2;
        spawnCenter = landingPlanet.transform.position;
    }

    public void Launch()
    {
        Initialize();
        if (Generate)
        {
            //InvokeRepeating(nameof(Spawn), LaunchTime, (1 / SpawnPerSec));
            Invoke(nameof(Spawn), LaunchTime);
            if(isIncrease)
            {
                //Invoke(nameof(updateSpawnAmount), LaunchTime);
                Invoke(nameof(Escalate), LaunchTime);
                //Invoke(nameof(IncreaseAcceleration), LaunchTime);
            }
            
        }
    }

    private void FixedUpdate()
    {
        playerPos = player.transform.position;
        //myTime += Time.deltaTime;
    }

    void Spawn()
    {
        Vector3 spawnPoint;
        Meteor mtr;

        // Select spawn location
        float rand = Random.value;
        if(rand <= attackToPlayerRatio)
        {
            spawnPoint = spawnCenter + playerPos * range * 2f;
        }
        else
        {
            spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;
        }
        mtr = Instantiate(meteor, spawnPoint, Quaternion.identity);
        mtr.getInfo(playerPos, dangerIndex);
        //mtr.randomize(acceleration);
        if(rand <= attackToPlayerRatio)
        {
            mtr.selectPlayerAsTarget(true);
        }
        else
        {
            mtr.selectPlayerAsTarget(false);
        }

        // Decide when to launch next meteor
        float nextMeteor = 1;
        float frequency = Random.value;
        if(frequency < attackInterval) 
        {
            nextMeteor = Random.Range((1 / SpawnPerSec), 1);
        }
        else
        {
            nextMeteor = Random.Range(1, 3);
        }
        
        Invoke(nameof(Spawn), nextMeteor);
        Debug.Log("Current SpawnPerSec: " + SpawnPerSec);
    }

    void Escalate()
    {
        if(dangerIndex < 20)
        {
            dangerIndex++;
        }
        if(attackInterval < .9f)
        {
            attackInterval += .05f;
        }

        if(SpawnPerSec < 8)
        {
            SpawnPerSec++;
        }

        if (attackToPlayerRatio < .9f)
        {
            attackToPlayerRatio += .05f;
        }

        Invoke(nameof(Escalate), escalatePeriod);
    }
}
