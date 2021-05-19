using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSoundManager : MonoBehaviour
{
    public static FightSoundManager Instance;
    public AudioSource audioSourceA;
    public AudioSource audioSourceB;
    public AudioSource audioSourceC;
    public List<AudioClip> audioClips;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClips(int i)
    {
        //Number 0 = phaseC_damages
        //Number 1 = phaseC_heal
        //Number 2 = phaseC_protection
        //Number 3 = phaseC_combo
        //Number 4 = phaseC_break
        //Number 5 = phaseC_acceleration
        //Number 6 = phaseC_slowdown
        //Number 7 = phaseC_paralysis
        //Number 8 = phaseC_silence
        //Number 9 = phaseC_cramp
        //Number 10 = phaseC_drain
        //Number 11 = phaseC_confusion
        //Number 12 = phaseC_curse
        //Number 13 = phaseC_battleCry
        //Number 14 = phaseC_charm
        //Number 15 = phaseC_frost
        //Number 16 = phaseC_divineTouch
        //Number 17 = phaseC_windstorm

        if (!audioSourceA.isPlaying && !audioSourceB.isPlaying && !audioSourceC.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (!audioSourceA.isPlaying && !audioSourceB.isPlaying && audioSourceC.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (!audioSourceA.isPlaying && audioSourceB.isPlaying && !audioSourceC.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (audioSourceA.isPlaying && !audioSourceB.isPlaying && !audioSourceC.isPlaying)
        {
            audioSourceB.clip = audioClips[i];
            audioSourceB.Play();
        }
        else if (!audioSourceA.isPlaying && audioSourceB.isPlaying && audioSourceC.isPlaying)
        {
            audioSourceB.clip = audioClips[i];
            audioSourceB.Play();
        }
        else if (audioSourceA.isPlaying && !audioSourceB.isPlaying && audioSourceC.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (audioSourceA.isPlaying && audioSourceB.isPlaying && !audioSourceC.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (audioSourceA.isPlaying && audioSourceB.isPlaying && audioSourceC.isPlaying)
        {
            audioSourceC.clip = audioClips[i];
            audioSourceC.Play();
        }
    }
}
