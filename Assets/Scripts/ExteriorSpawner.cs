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
    private float attackInterval;
    private int dangerIndex;
    //private float myTime;

    const float escalatePeriod = 10f; // every n seconds update the index
    private int acceleration;
    private float attackToPlayerRatio;
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

    private void Initialize()
    {
        //myTime = 0;
        dangerIndex = 0;
        acceleration = 1;
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
        //Vector3 spawnPoint = spawnCenter + (Random.onUnitSphere * spawnRadius) * range;

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
            nextMeteor = Random.Range(1, 4);
        }
        
        Invoke(nameof(Spawn), nextMeteor);
        Debug.Log("Current SpawnPerSec: " + SpawnPerSec);
    }
    /*
    void IncreaseAcceleration()
    {
        if (acceleration < 100)
        {
            acceleration += 1;
        }

        Invoke(nameof(IncreaseAcceleration), escalatePeriod / 10);
    }*/

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

    /*
    void updateSpawnAmount()
    {
        if (SpawnPerSec < 10)
        {
            SpawnPerSec++;
        }
        
        Invoke(nameof(updateSpawnAmount), 5);
    }*/

}
