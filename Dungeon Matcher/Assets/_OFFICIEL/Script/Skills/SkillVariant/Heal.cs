using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : Skill
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
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());

            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerHealFeedback(comboEffectValue));
        }
        else
        {
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerHealFeedback(effectValue));
        }

        if(Player.Instance.health < 0)
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
        Player.Instance.canAttack = true;
        comesFromCombo = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        PlaySound();
        //Combo effect
        if (comesFromCombo)
        {
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());

            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback(comboEffectValue));
        }
        else
        {
            SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiHealFeedback(effectValue));
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


        comesFromCombo = false;
        Enemy.Instance.canAttack = true;
        Enemy.Instance.lastEnemyCompetence = this;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseHeal = true;
    }

    public override void PlaySound()
    {
        Debug.Log("Son Heal");
        FightSoundManager.Instance.PlayClips(1);
    }
}
