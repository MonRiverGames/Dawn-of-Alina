using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource source;
    public AudioClip step;
    public AudioClip jumpUp;
    public AudioClip jumpDown;
    public AudioClip magicBeam;
    public AudioClip magicClap;
    public AudioClip magicSpiritFingers;

    void PlayAudioStep()
    {
        source.clip = step;
        source.Play();
    }

    void PlayAudioJumpUp()
    {
        source.clip = jumpUp;
        source.Play();
    }

    void PlayAudioJumpDown()
    {
        source.clip = jumpDown;
        source.Play();
    }

    void PlayAudioMagicBeam()
    {
        source.clip = magicBeam;
        source.Play();
    }

    void PlayAudioMagicClap()
    {
        source.clip = magicClap;
        source.Play();
    }

    void PlayAudioMagicSpiritFingers()
    {
        source.clip = magicSpiritFingers;
        source.Play();
    }
}