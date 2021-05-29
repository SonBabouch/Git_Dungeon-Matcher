using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/BasicAttackTuto")]
public class BasicAttackTuto : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;

    public override void Initialize(GameObject obj)
    {
        energyCost = initialEnergyCost;
        crampEnergyCost = initialEnergyCost + 10;
        owner = obj;
        owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    {
        realUse();
    }



    public override void InUse()
    {
        realInUse(skillIndex);
    }

    public override void PlayerEffect()
    {
        PlaySound();
        PlayerTuto.Instance.AllyAlteration();

        if (EnemyTuto.Instance.isDefending == false)
        {
            if (comesFromCombo)
            {
                if (EnemyTuto.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiAttackFeedBack());
                    EnemyTuto.Instance.health += (comboEffectValue * PlayerTuto.Instance.boostAttack) / 2;
                }
                else
                {
                    SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiAttackFeedBack());
                    EnemyTuto.Instance.health += comboEffectValue * PlayerTuto.Instance.boostAttack;
                }
            }
            else
            {
                if (EnemyTuto.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiAttackFeedBack());
                    EnemyTuto.Instance.health += effectValue * PlayerTuto.Instance.boostAttack;
                }
                else
                {
                    SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiAttackFeedBack());
                    EnemyTuto.Instance.health += effectValue * PlayerTuto.Instance.boostAttack;
                }
            }
        }

        if (EnemyTuto.Instance.health < 0)
        {
            EnemyTuto.Instance.health = 0;
        }


        if (!chargingAttack)
        {
            PlayerTuto.Instance.StopCoroutine(PlayerTuto.Instance.PlayerCombo());
            PlayerTuto.Instance.StartCoroutine(PlayerTuto.Instance.PlayerCombo());
        }

        if (messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        PlayerTuto.Instance.lastPlayerCompetence = this;
        comesFromCombo = false;
        PlayerTuto.Instance.canAttack = true;
        EnemyTuto.Instance.isDefending = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        PlaySound();
        if (PlayerTuto.Instance.isDefending == false)
        {
            if (comesFromCombo)
            {
                SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.PlayerAttackFeedBack());
                PlayerTuto.Instance.health += comboEffectValue * EnemyTuto.Instance.boostAttack;
            }
            else
            {
                SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.PlayerAttackFeedBack());
                PlayerTuto.Instance.health += effectValue * EnemyTuto.Instance.boostAttack;

            }
        }

        if (!chargingAttack)
        {
            EnemyTuto.Instance.StopCoroutine(EnemyTuto.Instance.EnemyCombo());
            EnemyTuto.Instance.StartCoroutine(EnemyTuto.Instance.EnemyCombo());
        }

        if (messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        EnemyTuto.Instance.lastEnemyCompetence = this;
        comesFromCombo = false;
        EnemyTuto.Instance.canAttack = true;
        PlayerTuto.Instance.isDefending = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        EnemyTuto.Instance.canUseAttack = true;
    }

    public override void PlaySound()
    {
        //Debug.Log("Son Attaque");
        FightSoundManager.Instance.PlayClips(0);
    }
}




