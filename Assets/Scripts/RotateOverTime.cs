using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    private Vector3 m_rotation;
    public float rotateSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        m_rotation = new Vector3(Random.value, Random.value, Random.value);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(m_rotation * Time.deltaTime * rotateSpeed);
        Debug.Log(transform.rotation.ToString());
    }
}
