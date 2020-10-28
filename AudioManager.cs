using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audio;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.threeDAudio;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
        }
        GameManager.manager.audioLoaded = true;
        Debug.Log("done");
    }
    public void Play(string name)
    {
        Sound sound = System.Array.Find(sounds, s => s.name.Equals(name));
        sound.source.Play();
    }
    public void Play(int id)
    {
        Sound sound = sounds[id];
        sound.source.Play();
    }
}
