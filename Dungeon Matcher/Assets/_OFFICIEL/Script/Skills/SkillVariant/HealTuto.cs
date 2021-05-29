using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/HealTuto")]
public class HealTuto : Skill
{
    public Skill himSelf;

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
        realUse();
    }

    public override void InUse()
    {
        realInUse(skillIndex);
    }

    public override void PlayerEffect()
    {
        PlaySound();
        //Combo effect
        if (comesFromCombo)
        {
            SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.PlayerHealFeedback(comboEffectValue));
        }
        else
        {
            SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.PlayerHealFeedback(effectValue));
        }

        if (PlayerTuto.Instance.health < 0)
        {
            PlayerTuto.Instance.health = 0;
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
        PlayerTuto.Instance.canAttack = true;
        comesFromCombo = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        PlaySound();
        //Combo effect
        if (comesFromCombo)
        {
            SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiHealFeedback(comboEffectValue));
        }
        else
        {
            SkillFeedbackTuto.Instance.StartCoroutine(SkillFeedbackTuto.Instance.EnemiHealFeedback(effectValue));
        }

        if (EnemyTuto.Instance.health < 0)
        {
            EnemyTuto.Instance.health = 0;
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


        comesFromCombo = false;
        EnemyTuto.Instance.canAttack = true;
        EnemyTuto.Instance.lastEnemyCompetence = this;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        EnemyTuto.Instance.canUseHeal = true;
    }

    public override void PlaySound()
    {
        Debug.Log("Son Heal");
        FightSoundManager.Instance.PlayClips(1);
    }
}

