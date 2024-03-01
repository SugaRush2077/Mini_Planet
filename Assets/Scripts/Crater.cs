using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    public int destroy_time = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyCrater), destroy_time);
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
