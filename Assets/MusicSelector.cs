using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioClip[] MenuMusicList;
    public AudioClip[] GameMusicList;
    AudioSource audio;
    bool isInGame = false;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        randomMenuMusic();
    }

    void Update()
    {
        if(!audio.isPlaying)
        {
            if(isInGame)
            {
                randomGameMusic();
            }
            else
            {
                randomMenuMusic();
            }
        }
    }
    public void switchToMenuMusic()
    {
        isInGame = false;
        audio.Stop();
        randomMenuMusic();
    }

    public void switchToGameMusic()
    {
        isInGame = true;
        audio.Stop();
        randomGameMusic();
    }

    void randomMenuMusic()
    {
        int rand = Random.Range(0, MenuMusicList.Length);
        audio.clip = MenuMusicList[rand];
        audio.Play();
        
    }
    void randomGameMusic()
    {
        int rand = Random.Range(0, GameMusicList.Length);
        audio.clip = GameMusicList[rand];
        audio.Play();

    }
}
