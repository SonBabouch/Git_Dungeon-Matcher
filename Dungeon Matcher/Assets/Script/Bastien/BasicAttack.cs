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
        switch(monster.side)
        {
            case MonsterToken.monsterSide.Ally:
                CombatManager.Instance.combatList[0].health += damage;
                break;
            case MonsterToken.monsterSide.Enemy:
                Player.Instance.health += damage;
                break;
        }

    }
}
