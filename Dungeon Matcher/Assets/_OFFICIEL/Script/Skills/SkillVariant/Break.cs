using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Break")]
public class Break : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;
    public override void Initialize(GameObject obj)
    {
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
        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
        CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        Player.Instance.StopCoroutine(Player.Instance.PlayerChargeAttack(Player.Instance.lastPlayerCompetence));

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
        CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseBreak = true;
    }
}
