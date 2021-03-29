using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : Skill
{
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;
    public override void Initialize(GameObject obj)
    {
        owner = obj;
        owner.GetComponent<MonsterToken>();
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
        Player.Instance.health -= healthAmount;
        Player.Instance.lastPlayerCompetence = this;
    }

    public override void MonsterEffect()
    {
        Enemy.Instance.health -= healthAmount;
        //Monsterer.Instance.lastMonsterCompetence = this;
    }

    public override void InUse()
    {
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.AllyAlteration();
                    Player.Instance.energy -= energyCost;
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
