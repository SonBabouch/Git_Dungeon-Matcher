using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    /// <summary>
    /// Template de Monstre
    /// </summary>
    
    [CreateAssetMenu(fileName = "Monster", menuName = "MonsterCard", order = 1)]
    public class MonsterCard : ScriptableObject
    {
        public string description;
        public string name;
        public float health;

        public enum raretyEnum {Common, Rare};
        public raretyEnum rarety;

        public Sprite profilPicture;
    }
}


