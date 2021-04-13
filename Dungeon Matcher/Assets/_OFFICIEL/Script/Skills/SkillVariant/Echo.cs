using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Echo")]
public class Echo : Skill
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
        CombatManager.Instance.ButtonsUpdate();

        typeOfCapacity = capacityType.Echo;
        chargingAttack = false;
        isComboSkill = false;
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
        Player.Instance.lastPlayerCompetence.PlayerEffect();

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        typeOfCapacity = capacityType.Echo;
        isComboSkill = false;
        chargingAttack = false;
    }

    public override void MonsterEffect()
    {
        //potentiellement yield return 0.1 secondes -> Coroutine
        Enemy.Instance.lastEnemyCompetence.MonsterEffect();

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }
        
        typeOfCapacity = capacityType.Echo;
        isComboSkill = false;
        chargingAttack = false;
        
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseEcho = true;
    }
}
