using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Plagiat")]
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
        if (Player.Instance.lastPlayerCompetence != null)
        {
            if (isEcho)
            {
                UpdateEchoValue();
            }

            if (isPlagiat)
            {
                UpdatePlagiatValue();
            }

            realUse();
        }
    }
    public override void InUse()
    {
        realInUse(skillIndex);
    }

    public override void PlayerEffect()
    {
        //potentiellement yield return 0.1 secondes -> Coroutine
        if(lastCompetenceReference != null)
        {
            lastCompetenceReference.PlayerEffect();
        }    

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

        Player.Instance.lastPlayerCompetence = this;
        Player.Instance.canAttack = true;

        isComboSkill = false;
        messageType = typeOfMessage.Small;
        typeOfCapacity = capacityType.Plagiat;
        isPlagiat = true;
        isEcho = false;
        lastCompetenceReference = null;

        //CombatManager.Instance.ButtonsUpdate();
    }
    public override void MonsterEffect()
    {
        lastCompetenceReference.MonsterEffect();

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        Enemy.Instance.lastEnemyCompetence = this;
        Enemy.Instance.canAttack = true;
        isComboSkill = false;
        messageType = typeOfMessage.Small;
        typeOfCapacity = capacityType.Plagiat;
        isPlagiat = true;
        isEcho = false;
        lastCompetenceReference = null;

        //CombatManager.Instance.ButtonsUpdate();
    }

    public override void SetEnemyBoolType()
    {
        Enemy.Instance.canUsePlagiat = true;
    }
}
