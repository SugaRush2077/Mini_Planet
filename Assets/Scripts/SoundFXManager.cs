using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    // [SerializeField] private AudioSource soundFXObject; // 不再需要，將在 ObjectPooler 中配置

    public AudioSource buttonSound;
    //public AudioSource landingSound;

    public void playButtonClickSound()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }
    }

    


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // 從物件池生成音效物件
        GameObject audioObject = ObjectPooler.Instance.SpawnFromPool("SoundFX", spawnTransform.position, Quaternion.identity);
        if (audioObject != null)
        {
            PooledAudioSource pooledAudio = audioObject.GetComponent<PooledAudioSource>();
            if (pooledAudio != null)
            {
                pooledAudio.Play(audioClip, volume, "SoundFX");
            }
        }
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClipArray, Transform spawnTransform, float volume)
    {
        // assign a random index
        int rand = Random.Range(0, audioClipArray.Length);
        AudioClip clipToPlay = audioClipArray[rand];

        // 從物件池生成音效物件
        GameObject audioObject = ObjectPooler.Instance.SpawnFromPool("SoundFX", spawnTransform.position, Quaternion.identity);
        if (audioObject != null)
        {
            PooledAudioSource pooledAudio = audioObject.GetComponent<PooledAudioSource>();
            if (pooledAudio != null)
            {
                pooledAudio.Play(clipToPlay, volume, "SoundFX");
            }
        }
    }


}
