using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Break")]
public class Break : Skill
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
        realUse();
    }

    public override void InUse()
    {
        realInUse(skillIndex);
    }

    public override void PlayerEffect()
    {
        PlaySound();
        Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));
        if (Enemy.Instance.isCharging)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));
            Enemy.Instance.StopCoroutine(Enemy.Instance.EndEnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));
            Enemy.Instance.lastEnemyCompetence.messageType = typeOfMessage.Charging;

            if (!chargingAttack)
            {
                Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
                Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
            }

            Player.Instance.lastPlayerCompetence = this;
            Player.Instance.canAttack = true;

            ConversationManager.Instance.enemyChargingAttack.SetActive(false);
            Enemy.Instance.canAttack = true;
            ConversationManager.Instance.canAttack = true;
            Enemy.Instance.isCharging = false;
            //Lancer l'animation
        }
    }

    public override void MonsterEffect()
    {
        if (Player.Instance.isCharging)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerChargeAttack(Player.Instance.lastPlayerCompetence));
            Player.Instance.StopCoroutine(Player.Instance.EndPlayerChargeAttack(Player.Instance.lastPlayerCompetence));
            Player.Instance.lastPlayerCompetence.messageType = typeOfMessage.Charging;
        PlaySound();
        Player.Instance.StopCoroutine(Player.Instance.PlayerChargeAttack(Player.Instance.lastPlayerCompetence));

            if (!chargingAttack)
            {
                Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
                Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
            }

            Enemy.Instance.lastEnemyCompetence = this;
            Enemy.Instance.canAttack = true;

            ConversationManager.Instance.playerChargingAttack.SetActive(false);
            ConversationManager.Instance.canAttack = true;
            Player.Instance.canAttack = true;
            Player.Instance.isCharging = false;
        }
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUseBreak = true;
    }

    public override void PlaySound()
    {
        FightSoundManager.Instance.PlayClips(4);
    }
}
