using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip Lwalk;
    public AudioClip Rwalk;

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    void Update()
    {
        
    }
    public void WalkLeftSound()
    {
        audio.clip = Lwalk;
        //audio.Stop();
        //audio.volume = 0.1f;
        audio.Play();
    }
    public void WalkRightSound()
    {
        audio.clip = Rwalk;
        //audio.Stop();
        //audio.volume = 0.1f;
        audio.Play();
    }
}
