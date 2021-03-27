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
                    ConversationManager.Instance.SendMessagesPlayer(this);
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
                    ConversationManager.Instance.SendMessagesEnemy(this);
                }
                break;
        }
        CombatManager.Instance.index = 0;
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
}
