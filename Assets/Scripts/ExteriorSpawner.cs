using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorSpawner : MonoBehaviour
{
    public GameObject planet;
    public Meteor meteor;

    public bool Generate = true;
    public float spawnPeriod = 1f;
    public float range = 5f;
    private Vector3 spawnCenter = Vector3.zero;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = planet.transform.localScale.x;
        spawnCenter = planet.transform.position;
        Debug.Log(spawnCenter);
        if (Generate)
        {
            InvokeRepeating(nameof(Spawn), 1f, spawnPeriod);
        }
        
    }

    void Spawn()
    {
        Vector3 spawnPoint = Random.onUnitSphere * radius * range;
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
