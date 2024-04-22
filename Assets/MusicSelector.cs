using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioClip[] musicList;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        randomMusic();
    }

    void Update()
    {
        if(!audio.isPlaying)
        {
            randomMusic();
        }
    }

    void randomMusic()
    {
        int rand = Random.Range(0, musicList.Length);
        audio.clip = musicList[rand];
        audio.Play();
        
    }
}
