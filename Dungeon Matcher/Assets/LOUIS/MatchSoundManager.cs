using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSoundManager : MonoBehaviour
{
    public static MatchSoundManager Instance;
    public AudioSource audioSourceA;
    public AudioSource audioSourceB;
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

    public void Update()
    {
        if (OnOffButton.Instance.isOn)
        {
            audioSourceA.mute = false;
            audioSourceB.mute = false;
        }
        else
        {
            audioSourceA.mute = true;
            audioSourceB.mute = true;
        }
    }

    public void PlayClips(int i)
    {
        //Number 0 = phaseM_swipeL V
        //Number 1 = phaseM_swipeR V
        //Number 2 = phaseM_clicksSwipeScreen 
        //Number 3 = phaseM_clicksConquestsListScreen
        //Number 4 = phaseM_clicksInventoryScreen
        //Number 5 = phaseM_clicksInventoryEquipmentChanged
        //Number 6 = phaseM_clicksOptions 
        //Number 7 = phaseM_screenTraveling
        //Number 8 = phaseM_rareGaugeFull 
        //Number 9 = phaseM_rareGaugeIncreasing 
        //Number 10 = phaseM_rareMonsterAppearing
        //Number 11 = phaseM_matchAlert
        //Number 12 = phaseM_noMoreMatch V
        //Number 13 = phaseM_ConquestsListAccepting
        //Number 14 = phaseM_ConquestsListRefusing

        if (!audioSourceA.isPlaying && !audioSourceB.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
        else if (audioSourceA.isPlaying && !audioSourceB.isPlaying)
        {
            audioSourceB.clip = audioClips[i];
            audioSourceB.Play();
        }
        else if (!audioSourceA.isPlaying && audioSourceB.isPlaying)
        {
            audioSourceA.clip = audioClips[i];
            audioSourceA.Play();
        }
    }
}
