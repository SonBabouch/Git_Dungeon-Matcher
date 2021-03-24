using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    { 
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.AllyAlteration();
                    Player.Instance.energy -= energyCost;
                    Player.Instance.health -= healthAmount;
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;
                    Enemy.Instance.health -= healthAmount;
                    ConversationManager.Instance.SendMessagesEnemy(this);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }
}
