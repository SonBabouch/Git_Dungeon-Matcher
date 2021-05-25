using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Slowdown")]
public class Slowdown : Skill
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
        Enemy.Instance.isSlowed = true;
        Player.Instance.lastPlayerCompetence = this;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
    }

    public override void MonsterEffect()
    {
        PlaySound();
        Player.Instance.isSlowed = true;
        Enemy.Instance.lastEnemyCompetence = this;

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseSlowdown = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(6);
    }
}
