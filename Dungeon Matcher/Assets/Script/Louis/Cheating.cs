﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Cheating")]
public class Cheating : Skill
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
        if (ConversationManager.Instance.canAttack)
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
    }

    public override void PlayerEffect()
    {
        //potentiellement yield return 0.1 secondes -> Coroutine
        Enemy.Instance.lastMonsterCompetence.PlayerEffect();
        Player.Instance.lastPlayerCompetence = this;
    }
    public override void MonsterEffect()
    {
        Player.Instance.lastPlayerCompetence.MonsterEffect();
        Enemy.Instance.lastMonsterCompetence = this;
    }

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;
                    Player.Instance.AllyAlteration();
                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this,0);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;
                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this,0);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

    
}
