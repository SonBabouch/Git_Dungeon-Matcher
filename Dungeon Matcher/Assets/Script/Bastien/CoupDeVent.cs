﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/CoupDeVent")]

public class CoupDeVent : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;

    public override void Initialize(GameObject obj)
    {
        energyCost = initialEnergyCost;
        crampEnergyCost = initialEnergyCost + 1;
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

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;

                    if (Player.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this, 7);
                }
                break;

            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;

                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this, 7);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

    public override void PlayerEffect()
    {
        Player.Instance.AllyAlteration();

        Enemy.Instance.EnemySwapSkill(0);
        Enemy.Instance.EnemySwapSkill(1);
        Enemy.Instance.EnemySwapSkill(2);
        Enemy.Instance.EnemySwapSkill(3);

        Player.Instance.lastPlayerCompetence = this;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
    }

    public override void MonsterEffect()
    {
        Player.Instance.PlayerSwapSkill(0);
        Player.Instance.PlayerSwapSkill(1);
        Player.Instance.PlayerSwapSkill(2);
        Player.Instance.PlayerSwapSkill(3);

        Enemy.Instance.lastEnemyCompetence = this;

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
    }


}