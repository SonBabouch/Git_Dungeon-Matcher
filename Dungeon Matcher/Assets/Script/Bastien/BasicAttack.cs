using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/basic Attack")]
public class BasicAttack : Skill
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
        if (Player.Instance.isCharging == false)
        {
            if (chargingAttack)
            {
                Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));
            }
            else
            {
                InUse();
            }
        }      
    }

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;
                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;
                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

    public override void PlayerEffect()
    {
        Enemy.Instance.health += healthAmount;
        Player.Instance.lastPlayerCompetence = this;
    }

    public override void MonsterEffect()
    {
        Player.Instance.health += healthAmount;
    }

    
}
