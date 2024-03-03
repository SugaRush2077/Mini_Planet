using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{


    public float destroy_time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyCrater), destroy_time);
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }
}

    

