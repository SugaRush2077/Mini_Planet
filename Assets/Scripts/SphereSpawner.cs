using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject planet;
    public GameObject obstacle;
    public float spawnPeriod = 1f;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = planet.transform.localScale.x;
        InvokeRepeating(nameof(Spawn), 1f, spawnPeriod);
    }

    void Spawn()
    {
        Instantiate(obstacle, Random.onUnitSphere * radius * 2, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
