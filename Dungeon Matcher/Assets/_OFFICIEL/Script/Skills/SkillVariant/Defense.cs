using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Defense")]

public class Defense : Skill
{

    //1- Variables
    //1.1- SerializeField
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;

    //2- Initialize
    public override void Initialize(GameObject obj)
    {

        owner = obj;

        monster = owner.GetComponent<MonsterToken>();

    }

    //3- Use: verification of differente conditions (eg: MonsterSide, Cramp, etc...)
    public override void Use()
    {
        realUse();
    }

    public override void InUse()
    {
        realInUse(skillIndex);
    }

    //4- Effect On the Player
    public override void PlayerEffect()
    {
        Player.Instance.isDefending = true;

        Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
        Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;
        CombatManager.Instance.ButtonsUpdate();
    }

    //5- Effect On The Monster
    public override void MonsterEffect()
    {

        Enemy.Instance.isDefending = true;

        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
        Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
        CombatManager.Instance.ButtonsUpdate();
    }

    //6- Launching of the attack
    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseDefense = true;
    }

}
