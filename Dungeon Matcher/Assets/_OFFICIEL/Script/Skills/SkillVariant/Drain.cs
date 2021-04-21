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
        Player.Instance.AllyAlteration();
        if(comesFromCombo)
        {
            Enemy.Instance.health += comboEffectValue * Player.Instance.boostAttack;
            Player.Instance.health -= comboEffectValue * Player.Instance.boostAttack;
        }
        else
        {
            Enemy.Instance.health += effectValue * Player.Instance.boostAttack;
            Player.Instance.health -= effectValue * Player.Instance.boostAttack;
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
        CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        if(comesFromCombo)
        {
            Player.Instance.health += comboEffectValue * Enemy.Instance.boostAttack;
            Enemy.Instance.health -= comboEffectValue * Enemy.Instance.boostAttack;
        }
        else
        {
            Player.Instance.health += effectValue * Enemy.Instance.boostAttack;
            Enemy.Instance.health -= effectValue * Enemy.Instance.boostAttack;
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
        CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseDrain = true;
    }
}
