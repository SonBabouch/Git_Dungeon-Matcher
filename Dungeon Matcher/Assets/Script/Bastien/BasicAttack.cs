using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

[CreateAssetMenu(menuName = "Skills/basic Attack")]
public class BasicAttack : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;
    [SerializeField]
    private float damage;


    public override void Initialize(GameObject obj)
    {
        owner = obj;
        monster = owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    {
        //Debug.Log(damage);
        //Debug.Log(side);
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy > energyCost)
                {
                    CombatManager.Instance.combatList[0].GetComponent<MonsterToken>().health += damage;
                    Player.Instance.energy -= energyCost;
                    Debug.Log(CombatManager.Instance.combatList[0].GetComponent<MonsterToken>().health);
                    Debug.Log(Player.Instance.energy);
                }
                break;
            case monsterSide.Enemy:
                if(CombatManager.Instance.enemyEnergy > energyCost)
                {
                    Player.Instance.health -= damage;
                }
                break;
        }

    }
}
