using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum Sound
{
    Music,
    Button,
    items,
    endGame
}
public class SoundManager :  MonoBehaviour
{
    AudioClip GetAudioClip(Sound s, int weight = 0)
    {
        if (s != Sound.items)
            return GameManager.instance.listAudioClips.Find(x => x.sound == s).audioClip;
        else
            return GameManager.instance.listAudioClips.Find(x => x.sound == s && x.itemWeight == weight).audioClip;
    }
    public void PlaySound(Sound s)
    {
        StopSound();
        GameManager.instance.sound.PlayOneShot(GetAudioClip(s));
    }
    public void PlaySoundItem(int weight)
    {
        StopSound();
        GameManager.instance.sound.PlayOneShot(GetAudioClip(Sound.items, weight));
    }
    public void PlayMusic()
    {
        StopSound();
        GameManager.instance.sound.clip = (GetAudioClip(Sound.Music));
        GameManager.instance.sound.Play();
        GameManager.instance.sound.loop = true;
    }
    public void StopSound()
    {
        if (GameManager.instance.sound.isPlaying)
            GameManager.instance.sound.Stop();
        GameManager.instance.sound.loop = false;
    }
}
[Serializable]
public class ListAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
    public int itemWeight;
}