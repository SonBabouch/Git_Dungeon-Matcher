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
        switch (side)
        {
            case monsterSide.Enemy:
                if (Enemy.Instance.isCharging = false && ConversationManager.Instance.canAttack)
                {
                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    //Test Curse
                    if (Enemy.Instance.isCurse)
                    {
                        int test = Random.Range(0, 100);
                        if (test < 10)
                        {
                            Enemy.Instance.energy -= energyCost;
                            Enemy.Instance.trueEnergy -= trueEnergyCost;

                            //switch la carte de la main de l'enemy;

                            break;
                        }
                        else
                        {
                            //=> Sinon ca fait la suite.

                            if (chargingAttack)
                            {
                                if (Enemy.Instance.energy >= energyCost)
                                {
                                    Enemy.Instance.energy -= energyCost;
                                    Enemy.Instance.trueEnergy -= trueEnergyCost;

                                    //ici ca sera Enemy plutot que player
                                    Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));
                                }
                            }
                            else
                            {
                                InUse();
                            }
                        }

                    }

                }
                break;

            case monsterSide.Ally:
                if (ConversationManager.Instance.canAttack && Player.Instance.isCharging == false)
                {
                    if (Player.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (Player.Instance.isCurse)
                    {
                        int test = Random.Range(0, 100);
                        if (test < 10)
                        {
                            Player.Instance.energy -= energyCost;
                            Player.Instance.trueEnergy -= trueEnergyCost;
                            CombatManager.Instance.ButtonsUpdate();
                            break;
                        }
                        else
                        {
                            if (chargingAttack)
                            {
                                if (Player.Instance.energy >= energyCost)
                                {
                                    Player.Instance.energy -= energyCost;
                                    Player.Instance.trueEnergy -= trueEnergyCost;

                                    //ici ca sera Enemy plutot que player
                                    Player.Instance.StartCoroutine(Player.Instance.ChargeAttack(this));
                                }
                            }
                            else
                            {
                                InUse();
                            }
                        }
                    }

                }
                break;
            default:
                break;
        }
    }

    public override void InUse()
    {
        //6.1-Verification of the monster side
        switch (side)
        {
            case monsterSide.Ally:

                if (Player.Instance.energy >= energyCost)
                {
                    Player.Instance.energy -= energyCost;
                    Player.Instance.trueEnergy -= trueEnergyCost;

                    if (Player.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    PlayerEffect();
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this, 7);
                }
                break;

            case monsterSide.Enemy:
                if (Enemy.Instance.energy >= energyCost)
                {
                    Enemy.Instance.energy -= energyCost;
                    Enemy.Instance.trueEnergy -= trueEnergyCost;

                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }

                    MonsterEffect();
                    ConversationManager.Instance.SendMessagesEnemy(this, 7);
                }
                break;
        }
        CombatManager.Instance.index = 0;
    }

    //4- Effect On the Player
    public override void PlayerEffect()
    {
        Player.Instance.isDefending = true;

        Player.Instance.lastPlayerCompetence = this;
        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }

    }

    //5- Effect On The Monster
    public override void MonsterEffect()
    {

        Enemy.Instance.isDefending = true;

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }
        Enemy.Instance.lastEnemyCompetence = this;

    }

    //6- Launching of the attack
    

}
