﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButton : MonoBehaviour
{
    public static OnOffButton Instance;
    public GameObject deathObject;
    public GameObject resultsObject;
    public bool isOn;

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

    public void Start()
    {
        isOn = true;
    }

    public void OnOffSwitcher()
    {
        if (isOn)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }
    }

    public void Update()
    {
        if (isOn)
        {
            MatchSoundManager.Instance.audioSourceA.enabled = true;
            MatchSoundManager.Instance.audioSourceB.enabled = true;
            MatchSoundManager.Instance.menuOSTSource.enabled = true;
            FightSoundManager.Instance.audioSourceA.enabled = true;
            FightSoundManager.Instance.audioSourceB.enabled = true;
            FightSoundManager.Instance.audioSourceC.enabled = true;
            FightSoundManager.Instance.fightOSTSource.enabled = true;
        }
        else
        {
            MatchSoundManager.Instance.audioSourceA.enabled = false;
            MatchSoundManager.Instance.audioSourceB.enabled = false;
            MatchSoundManager.Instance.menuOSTSource.enabled = false;
            FightSoundManager.Instance.audioSourceA.enabled = false;
            FightSoundManager.Instance.audioSourceB.enabled = false;
            FightSoundManager.Instance.audioSourceC.enabled = false;
            FightSoundManager.Instance.fightOSTSource.enabled = false;
        }
    }
}
