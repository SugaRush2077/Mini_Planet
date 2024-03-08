using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlanet : MonoBehaviour
{
    GameObject objToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        objToSpawn = new GameObject();
        objToSpawn.transform.parent = objToSpawn.transform;
        objToSpawn.transform.position = new Vector3(20, 0, 0);
        objToSpawn.name = "PCG_testPlanet";
        objToSpawn.AddComponent<Planet>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
