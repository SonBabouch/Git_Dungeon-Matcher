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
            case monsterSide.Enemy:
                if (Enemy.Instance.isCharging = false && ConversationManager.Instance.canAttack)
                {
                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (chargingAttack)
                    {
                        if (Enemy.Instance.energy >= energyCost)
                        {
                            Enemy.Instance.energy -= energyCost;

                            //ici ca sera Enemy plutot que player
                            Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));
                        }
                    }
                    else
                    {
                        InUse();
                    }
                }

                break;
            case monsterSide.Ally:

                if (ConversationManager.Instance.canAttack && Player.Instance.isCharging == false)
                {
                    if (Player.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (chargingAttack)
                    {
                        if (Player.Instance.energy >= energyCost)
                        {
                            Player.Instance.energy -= energyCost;

                            //ici ca sera Enemy plutot que player
                            Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));
                        }
                    }
                    else
                    {
                        InUse();
                    }
                }
                break;
            default:
                break;
        }
    }

    public override void PlayerEffect()
    {
        //Enemy.Instance.StopCoroutine(ChargeAttack());
        Player.Instance.lastPlayerCompetence = this;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
    }

    public override void MonsterEffect()
    {
        Player.Instance.StopCoroutine(Player.Instance.ChargeAttack(Player.Instance.lastPlayerCompetence));
        Enemy.Instance.lastEnemyCompetence = this;

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
    }

    public override void InUse()
    {
        if (Player.Instance.isCramp && side == monsterSide.Ally)
        {
            energyCost = crampEnergyCost;
        }

        if (Enemy.Instance.isCramp && side == monsterSide.Ally)
        {
            energyCost = crampEnergyCost;
        }

        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    if (Enemy.Instance.isCharging)
                    {
                        Player.Instance.AllyAlteration();
                        Player.Instance.energy -= energyCost;

                        if (Player.Instance.isCramp)
                        {
                            energyCost = initialEnergyCost;
                        }
                        PlayerEffect();
                    }
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
