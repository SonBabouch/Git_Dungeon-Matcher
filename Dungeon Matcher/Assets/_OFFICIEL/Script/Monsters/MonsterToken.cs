using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    [HideInInspector] public float minHealth;

    [HideInInspector] public enum raretyEnum { Common, Rare };
    [HideInInspector] public raretyEnum rarety;
    [HideInInspector] public enum statementEnum { Equipe,Claim, Disponible, Indisponible };
    public bool isGet;
    public statementEnum statement;

    public Sprite profilPicture;
    public Sprite fullMonsterImage;

    //Combat Stuff
    public enum monsterSide { Enemy, Ally }
    public monsterSide side;

    public GameObject owner;
    public List<Skill> allySkills;
    public List<Skill> ennemySkills;
    public string[] monsterHashTag;

    private void Start()
    {
        //Ajouter une bool qui switch selon ou l'on est dans la scene (Soit Combat / Soit Menu).
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

        statement = (statementEnum)template.statement;
        description = template.description;
        monsterName = template.monsterName;
        health = template.health;
        maxHealth = template.maxHealth;
        minHealth = template.minHealh;
        rarety = (raretyEnum)template.rarety;
        profilPicture = template.profilPicture;
        monsterIndexPosition = template.monsterIndexPosition;
    }

}

