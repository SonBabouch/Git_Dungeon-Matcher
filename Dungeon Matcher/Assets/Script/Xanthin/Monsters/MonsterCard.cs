﻿using UnityEngine;

namespace Monster
{
    /// <summary>
    /// XP_Template de Monstre (Scriptable Object)
    /// </summary>
    
    [CreateAssetMenu(fileName = "Monster", menuName = "MonsterCard", order = 1)]
    public class MonsterCard : ScriptableObject
    {
        #region Match Statement
        public string description;
        public string name;
        public float health;
        public enum raretyEnum {Common, Rare};
        public raretyEnum rarety;

        public Sprite profilPicture;
        #endregion

        #region Combat Statement
        public enum monsterSide {Enemy, Ally};
        public monsterSide side = monsterSide.Enemy;
        public bool inCombat = false;
        #endregion

        //public List<Skill> AllyMonster;
        //public List<Skill> EnemyMonster;
    }
}

