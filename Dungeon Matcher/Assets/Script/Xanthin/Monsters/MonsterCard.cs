<<<<<<< develop
﻿using UnityEngine;

namespace Monster
{
    /// <summary>
    /// XP_Template de Monstre (Scriptable Object)
    /// </summary>
    
    [CreateAssetMenu(fileName = "Monster", menuName = "MonsterCard", order = 1)]
    public class MonsterCard : ScriptableObject
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    /// <summary>
    /// Template de Monstre
    /// </summary>
    
    [CreateAssetMenu(fileName = "Monster", menuName = "MonsterCard", order = 1)]
    public class MonsterCard : ScriptableObject
>>>>>>> WIp Combat
    {
        #region Match Statement
        public string description;
        public string monsterName;
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
    }
}


