﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bastien Prigent
/// </summary>
public abstract class Skill : ScriptableObject
{
    public string skillName;
    public float damage;
    public enum monsterSide { Enemy, Ally }
    public monsterSide side;
    public float energyCost;
    public abstract void Initialize(GameObject obj);
    public abstract void Use();
}
