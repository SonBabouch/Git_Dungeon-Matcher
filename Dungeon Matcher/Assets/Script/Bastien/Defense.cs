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

        //3.1- Verification of the Side of the monster
        switch (side)
        {
            //a- Is Monster
            case monsterSide.Enemy:
                //a.1- verification if attack is not charging
                if(Enemy.Instance.isCharging == false && ConversationManager.Instance.canAttack)
                {
                    //a.1.1- Verifications of the cramp
                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }
                    //a.1.2- Verification if it's a charging attack
                    if (chargingAttack)
                    {

                        if(Enemy.Instance.energy >= energyCost)
                        {

                            Enemy.Instance.energy -= energyCost;
                            Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));

                        }

                    }
                    else
                    {
                        InUse();
                    }

                }

                break;
            //b- Is Ally
            case monsterSide.Ally:
                if(ConversationManager.Instance.canAttack && Player.Instance.isCharging == false)
                {

                    if (Player.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                }

                break;

        }

        if (Player.Instance.isCharging == false)
        {
            if (chargingAttack)
            {
                Player.Instance.ChargeAttack(this);
            }
            else
            {
                InUse();
            }
        }
    }

    //4- Effect On the Player
    public override void PlayerEffect()
    {

        Player.Instance.isDefending = true;
        CombatManager.Instance.ButtonsUpdate();

    }

    //5- Effect On The Monster
    public override void MonsterEffect()
    {

        Enemy.Instance.isDefending = true;
        CombatManager.Instance.ButtonsUpdate();

    }

    //6- Launching of the attack
    public override void InUse()
    {
        //6.1-Verification of the monster side
        switch (side)
        {
            case monsterSide.Ally:
                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;

                    if (Player.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    Player.Instance.AllyAlteration();
                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this,0);
                }
                break;
            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;

                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this,0);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

}
