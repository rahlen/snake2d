using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Sounds
{
    public enum Sound
    {
        SnakeMove,
        SnakeDie,
        SnakeEat,
        ButtonClick,
        ButtonOver,

    }

    public static void Playsound(Sound Sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(Sound));


    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {

            {
                if (soundAudioClip.Sound == sound)
                {
                    return soundAudioClip.AudioClip;
                }

            }
        }
                Debug.LogError("Sound " + sound + "Not Found");
                return null;
         





    }
}