using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Drain")]
public class Drain : Skill
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
        Player.Instance.AllyAlteration();
        if(comesFromCombo)
        {
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());

            Enemy.Instance.health += comboEffectValue * Player.Instance.boostAttack;
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerHealFeedback(comboEffectValue * Player.Instance.boostAttack));
        }
        else
        {
            Enemy.Instance.health += effectValue * Player.Instance.boostAttack;
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerHealFeedback(effectValue * Player.Instance.boostAttack));
        }

        if (Player.Instance.health < 0)
        {
            Player.Instance.health = 0;
        }

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        if (messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        Player.Instance.lastPlayerCompetence = this;
        comesFromCombo = false;
        Player.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        PlaySound();
        if (comesFromCombo)
        {
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());

            if (Enemy.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                Player.Instance.health += comboEffectValue * Player.Instance.boostAttack;
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());

                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback((comboEffectValue * Player.Instance.boostAttack) / 2));
            }
            else
            {
                Player.Instance.health += comboEffectValue * Player.Instance.boostAttack;
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());

                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback(comboEffectValue * Player.Instance.boostAttack));
            }
                    
        }
        else
        {
            if (Enemy.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                Player.Instance.health += effectValue * Enemy.Instance.boostAttack;
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());

                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback((effectValue * Player.Instance.boostAttack) / 2));
            }
            else
            {
                Player.Instance.health += effectValue * Player.Instance.boostAttack;
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());

                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback(effectValue * Player.Instance.boostAttack));
            }
        }

        if (Enemy.Instance.health < 0)
        {
            Enemy.Instance.health = 0;
        }

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        if (messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        Enemy.Instance.lastEnemyCompetence = this;
        comesFromCombo = false;
        Enemy.Instance.canAttack = true;
        Enemy.Instance.isDefending = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseDrain = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(10);
    }
}
