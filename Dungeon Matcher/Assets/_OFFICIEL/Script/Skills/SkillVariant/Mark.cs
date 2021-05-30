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
        switch (side)
        {
            case monsterSide.Ally:
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerMarkFeedback());
                break;
            case monsterSide.Enemy:
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiMarkFeedback());
                break;

        }
    }

    public override void PlayerEffect()
    {
        PlaySound();
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
        PlaySound();
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

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(13);
    }
}
