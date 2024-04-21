using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource soundFXObject;

    public AudioSource buttonSound;

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
        // spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // assign the audioClip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // get lenght of sound FX clip
        float clipLength = audioSource.clip.length;

        // destroy the clip after tit is done playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClipArray, Transform spawnTransform, float volume)
    {
        // assign a random index
        int rand = Random.Range(0, audioClipArray.Length);

        // spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // assign the audioClip
        audioSource.clip = audioClipArray[rand];

        // assign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // get lenght of sound FX clip
        float clipLength = audioSource.clip.length;

        // destroy the clip after tit is done playing
        Destroy(audioSource.gameObject, clipLength);
    }


}
