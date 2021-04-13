using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Cheating")]
public class Cheating : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;

    public override void Initialize(GameObject obj)
    {
        owner = obj;
        monster = owner.GetComponent<MonsterToken>();
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
        //potentiellement yield return 0.1 secondes -> Coroutine
        Enemy.Instance.lastEnemyCompetence.PlayerEffect();

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
    }
    public override void MonsterEffect()
    {
        Player.Instance.lastPlayerCompetence.MonsterEffect();

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUsePlagiat = true;
    }
}
