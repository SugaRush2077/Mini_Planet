using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    //public AudioSource explosion_audio;
    public int destroy_time = 5;
    // Start is called before the first frame update
    void Start()
    {
        //transform.RotateAround(Vector3.forward, 90f);
        Invoke(nameof(DestroyCrater), destroy_time);
        //explosion_audio.Play();
        
        //SoundFXManager.instance.PlaySoundFXClip(explosion_audioClip, transform, 1f);
    }

    private void DestroyCrater()
    {
        Destroy(gameObject);
    }
}
