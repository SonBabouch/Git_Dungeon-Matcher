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
        public string monsterName;
        [HideInInspector] public float health;
        [HideInInspector] public enum raretyEnum { Common, Rare };
        [HideInInspector] public raretyEnum rarety;
        [HideInInspector] public Sprite profilPicture;
        public enum monsterSide {Enemy, Ally}
        public monsterSide side;

        public GameObject owner;
        public List<Skill> allySkills;
        public List<Skill> ennemySkills;
        // Start is called before the first frame update
        void Awake()
        {
            description = template.description;
            monsterName = template.monsterName;
            health = template.health;
            rarety = (raretyEnum)template.rarety;
            profilPicture = template.profilPicture;
            side = (monsterSide)template.side;
        }

        private void Start()
        {
            foreach (Skill skill in allySkills)
            {
                skill.Initialize(owner);
            }
            foreach (Skill skill in ennemySkills)
            {
                skill.Initialize(owner);
            }
        }
    }
}

