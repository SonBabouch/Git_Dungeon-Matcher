using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Break")]
public class Break : Skill
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
        if (Player.Instance.isCharging == false)
        {
            if (chargingAttack)
            {
                Player.Instance.ChargeAttack(this);
            }
            else
            {
                InUse();
            }
        }
    }

    public override void PlayerEffect()
    {
        //Capa annulée.
        Player.Instance.lastPlayerCompetence = this;
    }

    public override void MonsterEffect()
    {
        //Capa annulée
        //Monsterer.Instance.lastMonsterCompetence = this;
    }

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    /*if (/*Enemy.Instance.isCharging)
                    {
                        Player.Instance.AllyAlteration();
                        Player.Instance.energy -= energyCost;
                        PlayerEffect();
                    }*/
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this,0);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    if (Player.Instance.isCharging)
                    {
                        Enemy.Instance.energy -= energyCost;
                        MonsterEffect();
                    }
                    ConversationManager.Instance.SendMessagesEnemy(this,0);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

   
}
