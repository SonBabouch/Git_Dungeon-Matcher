﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Echo")]
public class Echo : Skill
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

        typeOfCapacity = capacityType.Echo;
        chargingAttack = false;
        isComboSkill = false;
    }

    public override void Use()
    {
        if (Enemy.Instance.lastEnemyCompetence != null)
        {
            if (isEcho)
            {
                UpdateEchoValue();
            }

            if (isPlagiat)
            {
                UpdatePlagiatValue();
            }

            realUse();
        }
    }

    public override void InUse()
    {
        realInUse(skillIndex);
    }

    public override void PlayerEffect()
    {
        PlayerPlaySound();
        lastCompetenceReference.PlayerEffect();

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        typeOfCapacity = capacityType.Echo;
        isComboSkill = false;
        chargingAttack = false;
    }

    public override void MonsterEffect()
    {
        MonsterPlaySound();
        //potentiellement yield return 0.1 secondes -> Coroutine
        lastCompetenceReference.MonsterEffect();

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        messageType = typeOfMessage.Small;
        typeOfCapacity = capacityType.Plagiat;
        isPlagiat = true;
        isEcho = false;
        lastCompetenceReference = null;

    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseEcho = true;
    }

    public override void PlaySound()
    {

    }

    public void PlayerPlaySound()
    {
        Player.Instance.lastPlayerCompetence.PlaySound();
    }

    public void MonsterPlaySound()
    {
        Enemy.Instance.lastEnemyCompetence.PlaySound();
    }
}
