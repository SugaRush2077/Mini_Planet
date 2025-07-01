using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    //public AudioSource explosion_audio;
    public int destroy_time;
    // Start is called before the first frame update
    void Start()
    {
        // 不要再於 Start 中自動啟動計時器
    }

    public void Activate()
    {
        //transform.RotateAround(Vector3.forward, 90f);
        StartCoroutine(ReturnToPoolAfterDelay(destroy_time));
        //explosion_audio.Play();
        
        //SoundFXManager.instance.PlaySoundFXClip(explosion_audioClip, transform, 1f);
    }

    public void setScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject.activeInHierarchy) {
            ObjectPooler.Instance.ReturnToPool("Crater", gameObject);
        }
    }
}
