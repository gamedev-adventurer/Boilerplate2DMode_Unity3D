using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource[] efxSourceArray;


    [Header("Volume")]
    public float efxVolume;
    public float musicVolume;


    // Update is called once per frame
    void Update()
    {
        efxVolume = Mathf.Clamp(efxVolume, 0.0f, 1.0f);
        musicVolume = Mathf.Clamp(musicVolume, 0.0f, 1.0f);
        musicSource.volume = musicVolume;

        foreach (AudioSource source in efxSourceArray)
        {

            source.volume = efxVolume;
        }
    }

    public void PlaySingle(AudioClip clip)
    {
        foreach (AudioSource audioS in efxSourceArray)
        {

            if (audioS.isPlaying == false)
            {
                audioS.clip = clip;
                audioS.Play();
                break;

            }

        }

    }

    public void ChangeMusicClip(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}
