using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Mark")]
public class Mark : Skill
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
        Player.Instance.isBoosted = true;
        Player.Instance.lastPlayerCompetence = this;

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        Enemy.Instance.isBoosted = true;
        Enemy.Instance.lastEnemyCompetence = this;

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseMark = true;
    }
}
