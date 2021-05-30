using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/BasicAttack")]
public class BasicAttack : Skill
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
        Debug.Log("hello");
        if (Enemy.Instance.isDefending == false)
        {
            if(comesFromCombo)
            {
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());
                if(Enemy.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
                    Enemy.Instance.health += (comboEffectValue * Player.Instance.boostAttack)/2;
                }
                else
                {
                    SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
                    Enemy.Instance.health += comboEffectValue * Player.Instance.boostAttack;
                }
            }
            else
            {
                if (Enemy.Instance.currentMonster.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
                    Enemy.Instance.health += effectValue * Player.Instance.boostAttack;
                }
                else
                {
                    SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiAttackFeedBack());
                    Enemy.Instance.health += effectValue * Player.Instance.boostAttack;
                }
            }
        }

        if (Enemy.Instance.health < 0)
        {
            Enemy.Instance.health = 0;
        }


        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }
        
        if(messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        Player.Instance.lastPlayerCompetence = this;
        comesFromCombo = false;
        Player.Instance.canAttack = true;
        Enemy.Instance.isDefending = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        PlaySound();
        if (Player.Instance.isDefending == false)
        {
            if(comesFromCombo)
            {
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.ComboFeedback());

                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());
                Player.Instance.health += comboEffectValue * Enemy.Instance.boostAttack;
            }
            else
            {
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerAttackFeedBack());
                Player.Instance.health += effectValue * Enemy.Instance.boostAttack;

            }
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
        Player.Instance.isDefending = false;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseAttack = true;
    }

    public override void PlaySound()
    {
        //Debug.Log("Son Attaque");
        FightSoundManager.Instance.PlayClips(0);
    }
}

    

