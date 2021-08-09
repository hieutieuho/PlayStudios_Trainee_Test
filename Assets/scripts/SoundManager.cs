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
public static class SoundManager
{
    static AudioClip GetAudioClip(Sound s, int weight = 0){
        if(s != Sound.items)
            return GameManager.instance.listAudioClips.Find(x => x.sound == s).audioClip;
        else
            return GameManager.instance.listAudioClips.Find(x => x.sound == s && x.itemWeight == weight).audioClip;
    }   
    public static void PlaySound(Sound s)
    {
        StopSound();
        GameManager.instance.sound.PlayOneShot(GetAudioClip(s));
    }
    public static void PlaySoundItem(int weight)
    {
        StopSound();
        GameManager.instance.sound.PlayOneShot(GetAudioClip(Sound.items, weight));
    }
    public static void PlayMusic(){
        StopSound();
        GameManager.instance.sound.clip = (GetAudioClip(Sound.Music));
        GameManager.instance.sound.Play();
    }
    static void StopSound(){
        if(GameManager.instance.sound.isPlaying)
            GameManager.instance.sound.Stop();
    }
}
[Serializable]
public class ListAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
    public int itemWeight;
}