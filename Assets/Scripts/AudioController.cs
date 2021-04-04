using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioClip[] Audios;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }
    
    public void PlayMyAudio(int index, bool loop)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = Audios[index];
        audioSource.loop = loop;
        audioSource.Play();
    }

    private void Start()
    {
        PlayMyAudio(0, true);
    }
}
