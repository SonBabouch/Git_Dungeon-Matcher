using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterToken : MonoBehaviour
    {
        [SerializeField] private MonsterCard template;

        //Permet de le retrouver dans l'encyclopédie des monstres. Pour update les infomations dans le Menu Details & dans le Bag
        [HideInInspector] public int monsterIndexPosition;
        public int numberOfFragments;
        public int[] numberOfFragmentsRequired;
        public int monsterLevel;

        //Values From CardTemplate
        [HideInInspector] public string description;
        public string monsterName;
        [HideInInspector] public float health;
        [HideInInspector] public float maxHealth;
        [HideInInspector] public enum raretyEnum { Common, Rare };
        [HideInInspector] public raretyEnum rarety;

        [HideInInspector] public enum statementEnum { Equipe, Disponible, Indisponible };
         public statementEnum statement;

        [HideInInspector] public Sprite profilPicture;
         public Sprite fullMonsterImage;

        //Combat Stuff
        public enum monsterSide {Enemy, Ally}
        public monsterSide side;

        public GameObject owner;
        public List<Skill> allySkills;
        public List<Skill> ennemySkills;

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

        public void Initialize()
        {
            Debug.Log("Initialize 1");
            statement = (statementEnum)template.statement;
            description = template.description;
            monsterName = template.monsterName;
            health = template.health;
            rarety = (raretyEnum)template.rarety;
            profilPicture = template.profilPicture;
            side = (monsterSide)template.side;
            monsterIndexPosition = template.monsterIndexPosition;
            maxHealth = 100f;
        }
    }
}

