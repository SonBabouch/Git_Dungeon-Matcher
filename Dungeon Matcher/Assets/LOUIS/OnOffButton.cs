using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButton : MonoBehaviour
{
    public static OnOffButton Instance;
    public GameObject deathObject;
    public GameObject resultsObject;
    public bool isOn;

    public bool isTuto = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
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
        if (!isTuto)
        {
            if (isOn)
            {
                if (ManagerManager.Instance.menuManager.activeInHierarchy)
                {
                    MatchSoundManager.Instance.audioSourceA.enabled = true;
                    MatchSoundManager.Instance.audioSourceB.enabled = true;
                    MatchSoundManager.Instance.menuOSTSource.enabled = true;
                }

                if (ManagerManager.Instance.combatManager.activeInHierarchy)
                {
                    FightSoundManager.Instance.audioSourceA.enabled = true;
                    FightSoundManager.Instance.audioSourceB.enabled = true;
                    FightSoundManager.Instance.audioSourceC.enabled = true;
                    FightSoundManager.Instance.fightOSTSource.enabled = true;
                }
            }
            else
            {
                if (ManagerManager.Instance.menuManager.activeInHierarchy)
                {
                    MatchSoundManager.Instance.audioSourceA.enabled = false;
                    MatchSoundManager.Instance.audioSourceB.enabled = false;
                    MatchSoundManager.Instance.menuOSTSource.enabled = false;
                }

                if (ManagerManager.Instance.combatManager.activeInHierarchy)
                {
                    FightSoundManager.Instance.audioSourceA.enabled = false;
                    FightSoundManager.Instance.audioSourceB.enabled = false;
                    FightSoundManager.Instance.audioSourceC.enabled = false;
                    FightSoundManager.Instance.fightOSTSource.enabled = false;
                }
            }
        }
        else //C'est le tuto;
        {
            if (isOn)
            {
                if (TutorielManager.Instance.MenuGO.activeInHierarchy)
                {
                    MatchSoundManager.Instance.audioSourceA.enabled = true;
                    MatchSoundManager.Instance.audioSourceB.enabled = true;
                    MatchSoundManager.Instance.menuOSTSource.enabled = true;
                }

                if (TutorielManager.Instance.CombatGO.activeInHierarchy)
                {
                    FightSoundManager.Instance.audioSourceA.enabled = true;
                    FightSoundManager.Instance.audioSourceB.enabled = true;
                    FightSoundManager.Instance.audioSourceC.enabled = true;
                    FightSoundManager.Instance.fightOSTSource.enabled = true;
                }
            }
            else
            {
                if (TutorielManager.Instance.MenuGO.activeInHierarchy)
                {
                    MatchSoundManager.Instance.audioSourceA.enabled = false;
                    MatchSoundManager.Instance.audioSourceB.enabled = false;
                    MatchSoundManager.Instance.menuOSTSource.enabled = false;
                }

                if (TutorielManager.Instance.CombatGO.activeInHierarchy)
                {
                    FightSoundManager.Instance.audioSourceA.enabled = false;
                    FightSoundManager.Instance.audioSourceB.enabled = false;
                    FightSoundManager.Instance.audioSourceC.enabled = false;
                    FightSoundManager.Instance.fightOSTSource.enabled = false;
                }
            }
        }

        
    }
}
