using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;
    public override void Initialize(GameObject obj)
    {
        owner = obj;
        monster = owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.health += damage;
                    Player.Instance.energy -= energyCost;
                    if (Player.Instance.health > Player.Instance.maxHealth)
                    {
                        Player.Instance.health = Player.Instance.maxHealth;
                    }
                }
                break;
            case monsterSide.Enemy:
                if (CombatManager.Instance.enemyEnergy >= energyCost)
                {
                    monster.GetComponent<MonsterToken>().health += damage;
                    if(monster.GetComponent<MonsterToken>().health < monster.GetComponent<MonsterToken>().maxHealth)
                    {
                        monster.GetComponent<MonsterToken>().health = monster.GetComponent<MonsterToken>().maxHealth;
                    }
                }
                break;
        }
    }
}
