using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/CrampTuto")]
public class CrampTuto : Skill
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

    public override void MonsterEffect()
    {
        PlaySound();
        PlayerTuto.Instance.isCramp = true;
        //CombatManager.Instance.ButtonsUpdate();

        EnemyTuto.Instance.StopCoroutine(EnemyTuto.Instance.EnemyCombo());
        EnemyTuto.Instance.StartCoroutine(EnemyTuto.Instance.EnemyCombo());

        EnemyTuto.Instance.lastEnemyCompetence = this;
        EnemyTuto.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void PlayerEffect()
    {
        PlaySound();
        EnemyTuto.Instance.isCramp = true;
        EnemyTuto.Instance.lastEnemyCompetence = this;

        PlayerTuto.Instance.StopCoroutine(PlayerTuto.Instance.PlayerCombo());
        PlayerTuto.Instance.StartCoroutine(PlayerTuto.Instance.PlayerCombo());

        PlayerTuto.Instance.lastPlayerCompetence = this;
        PlayerTuto.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        EnemyTuto.Instance.canUseCramp = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(9);
    }
}
