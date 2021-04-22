using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/CoupDeVent")]

public class CoupDeVent : Skill
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
        Player.Instance.AllyAlteration();

        Enemy.Instance.EnemySwapSkill(0);
        Enemy.Instance.EnemySwapSkill(1);
        Enemy.Instance.EnemySwapSkill(2);
        Enemy.Instance.EnemySwapSkill(3);

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void MonsterEffect()
    {
        Player.Instance.PlayerSwapSkill(0);
        Player.Instance.PlayerSwapSkill(1);
        Player.Instance.PlayerSwapSkill(2);
        Player.Instance.PlayerSwapSkill(3);

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseCoupdeVent = true;
    }
}
