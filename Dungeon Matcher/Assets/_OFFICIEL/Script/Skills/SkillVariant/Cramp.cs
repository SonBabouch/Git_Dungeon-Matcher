using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Cramp")]
public class Cramp : Skill
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
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.PlayerCrampFeedback());
                break;
            case monsterSide.Enemy:
                SkillFeedback.Instance.StartCoroutine(SkillFeedback.Instance.EnemiCrampFeedback());
                break;
        
        }

    }

    public override void MonsterEffect()
    {
        PlaySound();
        Player.Instance.isCramp = true;
        //CombatManager.Instance.ButtonsUpdate();
        Debug.Log("hello");
        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void PlayerEffect()
    {
        PlaySound();
        Enemy.Instance.isCramp = true;
        Debug.Log("hello");
        Enemy.Instance.lastEnemyCompetence = this;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseCramp = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(9);
    }
}

 

