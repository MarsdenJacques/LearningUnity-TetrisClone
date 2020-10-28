﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audio;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    [Range(0f,1f)]
    public float threeDAudio;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    public bool playOnAwake;
    public Sound()
    {

    }
}
