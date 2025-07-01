using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    private string poolTag;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume, string tag)
    {
        this.poolTag = tag;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        StartCoroutine(ReturnToPoolAfterPlayback(clip.length));
    }

    private IEnumerator ReturnToPoolAfterPlayback(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject.activeInHierarchy) // 確保物件仍然處於活動狀態
        {
            ObjectPooler.Instance.ReturnToPool(poolTag, gameObject);
        }
    }
}
