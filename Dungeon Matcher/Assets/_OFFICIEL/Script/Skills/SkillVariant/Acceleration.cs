using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Acceleration")]
public class Acceleration : Skill
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
        Player.Instance.isAccelerated = true;
        Player.Instance.lastPlayerCompetence = this;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
    }

    public override void MonsterEffect()
    {
        PlaySound();
        Enemy.Instance.isAccelerated = true;
        Enemy.Instance.lastEnemyCompetence = this;

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseAcceleration = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(5);
    }
}
