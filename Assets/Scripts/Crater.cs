using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    public int destroy_time = 5;
    // Start is called before the first frame update
    void Start()
    {
        //transform.RotateAround(Vector3.forward, 90f);
        Invoke(nameof(DestroyCrater), destroy_time);
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }
}
