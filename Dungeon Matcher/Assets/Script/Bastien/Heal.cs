using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : Skill
{
    public Skill himSelf;

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

    public override void PlayerEffect()
    {
        //Combo effect
        if(Player.Instance.isCombo && isComboSkill)
        {
            Player.Instance.health -= comboEffectValue;
        }
        else
        {
            Player.Instance.health -= effectValue;
        }

        if(Player.Instance.health < 0)
        {
            Player.Instance.health = 0;
        }

        Player.Instance.lastPlayerCompetence = this;

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }
    }

    public override void MonsterEffect()
    {
        //Combo effect
        if (Enemy.Instance.isCombo && isComboSkill)
        {
            Enemy.Instance.health -= comboEffectValue;
        }
        else
        {
            Enemy.Instance.health -= effectValue;
        }

        if (Enemy.Instance.health < 0)
        {
            Enemy.Instance.health = 0;
        }
        Enemy.Instance.lastEnemyCompetence = this;

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }
    }

   

    
}
