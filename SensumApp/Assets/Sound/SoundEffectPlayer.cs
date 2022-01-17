using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public SoundEffects[] clips;
    public void Play(string name)
    {

        foreach (SoundEffects clip in clips)
        {
            if (name != clip.Name) continue;
            audioSource.PlayOneShot(clip.Clip);
            break;
        }
    }
}
[Serializable]
public class SoundEffects
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    public string Name => name;
    public AudioClip Clip => clip;
}