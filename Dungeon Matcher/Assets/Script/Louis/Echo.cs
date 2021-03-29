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
        owner = obj;
        monster = owner.GetComponent<MonsterToken>();
    }

    public override void Use()
    {
        if (chargingAttack)
        {
            coroutine.StartCoroutine(ChargeAttack());
        }
        else
        {
            InUse();
        }
    }

    public override void PlayerEffect()
    {
        Player.Instance.lastPlayerCompetence.PlayerEffect();
    }
    public override void MonsterEffect()
    {
        //potentiellement yield return 0.1 secondes -> Coroutine
        Enemy.Instance.lastMonsterCompetence.MonsterEffect();
        Enemy.Instance.lastMonsterCompetence = this;
    }

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;
                    Player.Instance.AllyAlteration();
                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;
                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

    public override IEnumerator ChargeAttack()
    {
        Player.Instance.isCharging = true;
        yield return new WaitForSeconds(ChargingTime);
        Player.Instance.isCharging = false;
        InUse();
    }
}
