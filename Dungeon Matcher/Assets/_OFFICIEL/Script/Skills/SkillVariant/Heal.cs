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
        //Combo effect
        if(comesFromCombo)
        {
            Player.Instance.health -= comboEffectValue;
        }
        else
        {
            Player.Instance.health -= effectValue;
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
        //Combo effect
        if (comesFromCombo)
        {
            Enemy.Instance.health -= comboEffectValue;
        }
        else
        {
            Enemy.Instance.health -= effectValue;
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
}
