using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class Heal : Skill
{
    [SerializeField]
    private MonsterToken monster;
    private MonsterToken monsterSide;
    [SerializeField]
    private GameObject owner;
    [SerializeField]
    private float healthValue;

    public override void Initialize(GameObject obj)
    {
        owner = obj;
        monster = owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    {
        /*switch (monster.side)
        {
            case 0:
                monster.health -= healthValue;
                break;
            case 1:
                Player.health -= healthValue;
                break;
            default:
                Debug.Log("Default");
                break;
        }*/
        //Switch permettant de soigner l'ennemi ou le joueur en fonction de qui lance le sort. Proposition non-définie. Enemy = 0 et Ally = 1 dans le script MonsterToken.

        monster.health -= healthValue;
    }
}

