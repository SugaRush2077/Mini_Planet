using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    //float duration = 10f;
    //float magnitude = 4f;
    private bool isLoop = true;

    private Vector3 pos1 = new Vector3(.26f, .3f, -80);
    private Vector3 pos2 = new Vector3(-.22f, .15f, -80);
    private Vector3 pos3 = new Vector3(-.11f, -.38f, -80);
    private Vector3 pos4 = new Vector3(.18f, -.20f, -80);

    private float Speed = 1f;

    void Start()
    {
        originalPos = transform.localPosition;
    }
    public void ShakeCam()
    {
        if(isLoop)
        {
            transform.position = Vector3.Lerp(pos1, pos2, Speed * Time.deltaTime);
            transform.position = Vector3.Lerp(pos2, pos3, Speed * Time.deltaTime);
            transform.position = Vector3.Lerp(pos3, pos4, Speed * Time.deltaTime);
            transform.position = Vector3.Lerp(pos4, pos1, Speed * Time.deltaTime);
        }
        //Vector3 originalPos = transform.localPosition;
        //float elapsedTime = 0.0f;
        /*
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;
        transform.localPosition = new Vector3(x, y, originalPos.z);
        //elapsedTime += Time.deltaTime;
         

        //transform.localPosition = originalPos;*/
        Invoke(nameof(ShakeCam), 3f);
    }

    public IEnumerator ShakePos(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public IEnumerator ShakeRotate(float duration, float magnitude)
    {
        Quaternion originalRotation = transform.rotation;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.rotation = Quaternion.Euler(x, y, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation;
    }
}
