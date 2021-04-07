using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bastien Prigent
/// </summary>
public abstract class Skill : ScriptableObject
{
    [Header("Common")]
    public string skillDescription;
    public float effectValue;
   
    public enum monsterSide { Enemy, Ally }
    public monsterSide side;

    public enum typeOfMessage { Small, Big, Emoji }
    public typeOfMessage messageType;

    public enum capacityType { Attack, Heal, Defense,Paralysie, DivinTouch, CoupDeVent, Drain, Echo, Plagiat, Mark,Curse,Cramp,Charm,Silence,Lock,Break};
    public capacityType typeOfCapacity;

    [Header("Energy Cost")]
    public int trueEnergyCost;
    public int energyCost;
    public int initialEnergyCost;
    public int crampEnergyCost;

    [Header("TypeOfCapacity")]
    public bool isComboSkill = false;
    public bool comesFromCombo = false;
    public float comboEffectValue;
    public bool chargingAttack;

    public bool isEcho;

    public abstract void Initialize(GameObject obj);
    public abstract void Use();
    public abstract void PlayerEffect();
    public abstract void MonsterEffect();
    public abstract void InUse();

    
}
