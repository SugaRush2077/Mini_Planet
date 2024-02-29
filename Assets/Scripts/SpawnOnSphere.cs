using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnSphere : MonoBehaviour
{
    // Old settings
    public GameObject prefab;
    public bool spawned = true;
    public float radius = 10f;

    // New

    
    private void Start()
    {
        Invoke(nameof(Spawn), 1);
        //Invoke(nameof(SpawnOriented), 1);
    }

    private void Spawn()
    {
        Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
        Debug.Log(randomDirection);
        Vector3 spawnPosition = transform.position + randomDirection * radius;

        RaycastHit hit;
        if (Physics.Raycast(spawnPosition, Vector3.up, out hit) || Physics.Raycast(spawnPosition, Vector3.down, out hit))
        {
            Vector3 final = hit.point;

            Quaternion orientation;
            orientation = Quaternion.LookRotation(hit.normal);

            Instantiate(prefab, spawnPosition, orientation);
            
        }
        Invoke(nameof(Spawn), 0.5f);
    }

    private void SpawnOriented()
    {
        Vector2 spawnPos_v2 = Random.onUnitSphere * radius;
        Vector3 spawnPos = new Vector3(spawnPos_v2.x, 0.0f, spawnPos_v2.y);
        Vector3 transformOffsetPos = transform.position + spawnPos;


        RaycastHit hit;
        if (Physics.Raycast(transformOffsetPos, Vector3.down, out hit))
        {
            Vector3 final = hit.point;

            Quaternion orientation;
            orientation = Quaternion.LookRotation(hit.normal);

            Instantiate(prefab, final, orientation);
        }
        Invoke(nameof(SpawnOriented), 0.5f);

    }
    private void Update()
    {
        
    }
}
