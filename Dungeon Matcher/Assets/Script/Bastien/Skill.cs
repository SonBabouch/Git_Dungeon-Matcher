﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bastien Prigent
/// </summary>
public abstract class Skill : ScriptableObject
{
    public SkillCoroutine coroutine;

    public string skillDescription;
    public float healthAmount;
    public enum monsterSide { Enemy, Ally }
    public monsterSide side;

    public enum typeOfMessage { Small, Big, Emoji }
    public typeOfMessage messageType;

    public float energyCost;

    //Charging Stuff
    public bool chargingAttack;
    public float ChargingTime;

    public abstract void Initialize(GameObject obj);
    public abstract void Use();
    public abstract void PlayerEffect();
    public abstract void MonsterEffect();
    public abstract void InUse();

    public abstract IEnumerator ChargeAttack();
    public abstract class SkillCoroutine : MonoBehaviour {
        public SkillCoroutine(){ }
    }

}
