using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterToken : MonoBehaviour
    {
        [SerializeField] private MonsterCard template;

        //Values From CardTemplate
        [HideInInspector] public string description;
        public string name;
        [HideInInspector] public float health;
        [HideInInspector] public enum raretyEnum { Common, Rare };
        [HideInInspector] public raretyEnum rarety;
        [HideInInspector] public Sprite profilPicture;

        // Start is called before the first frame update
        void Awake()
        {
            description = template.description;
            name = template.name;
            health = template.health;
            rarety = (raretyEnum)template.rarety;
            profilPicture = template.profilPicture;
        }
    }
}

