using UnityEngine;

namespace Monster
{
    /// <summary>
    /// XP_Template de Monstre (Scriptable Object)
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


