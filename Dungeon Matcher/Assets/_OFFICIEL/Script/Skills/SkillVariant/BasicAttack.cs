using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/BasicAttack")]
public class BasicAttack : Skill
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
    }

    public override void Use()
    {
        if (ConversationManager.Instance.canAttack)
        {
            switch (side)
            {
                case monsterSide.Enemy:

                    if (Enemy.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (Enemy.Instance.isCharging = false && Enemy.Instance.canAttack && Enemy.Instance.energy >= energyCost)
                    {
                        if (Enemy.Instance.isCombo && isComboSkill)
                        {
                            comesFromCombo = true;
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
                        }
                        //=> Sinon ca fait la suite.
                        if (chargingAttack)
                        {
                            if (Enemy.Instance.energy >= energyCost)
                            {
                                Enemy.Instance.energy -= energyCost;
                                Enemy.Instance.trueEnergy -= trueEnergyCost;

                                //ici ca sera Enemy plutot que player
                                Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyChargeAttack(this));
                            }
                        }
                        else
                        {
                            InUse();
                        }

                    }
                    break;

                case monsterSide.Ally:

                    if (Player.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (Player.Instance.canAttack && Player.Instance.isCharging == false && Player.Instance.energy >= energyCost)
                    {
                        if (Player.Instance.isCombo && isComboSkill)
                        {
                            comesFromCombo = true;
                        }

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

                        }
                        if (chargingAttack)
                        {
                            if (Player.Instance.energy >= energyCost)
                            {
                                Player.Instance.energy -= energyCost;
                                Player.Instance.trueEnergy -= trueEnergyCost;

                                //ici ca sera Enemy plutot que player
                                Player.Instance.StartCoroutine(Player.Instance.PlayerChargeAttack(this));
                            }
                        }
                        else
                        {
                            InUse();
                        }

                    }
                    break;
                default:
                    break;
            }
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

                    
                    CombatManager.Instance.ButtonsUpdate();
                    ConversationManager.Instance.SendMessagesPlayer(this, 0);
                    
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

                    
                    Enemy.Instance.EnemySwapSkill(Enemy.Instance.enemyIndex);
                    ConversationManager.Instance.SendMessagesEnemy(this, 0);
                    
                }
                break;
        }
        CombatManager.Instance.index = 0;
        Enemy.Instance.enemyIndex = 0;
    }

    public override void PlayerEffect()
    {
        Player.Instance.AllyAlteration();

        if (Enemy.Instance.isDefending == false)
        {
            if(Player.Instance.isCombo && isComboSkill)
            {
                Enemy.Instance.health += comboEffectValue * Player.Instance.boostAttack;
            }
            else
            {
                Enemy.Instance.health += effectValue * Player.Instance.boostAttack;
            }
        }
        Player.Instance.lastPlayerCompetence = this;

        if (!chargingAttack)
        {
            Player.Instance.StopCoroutine(Player.Instance.PlayerCombo());
            Player.Instance.StartCoroutine(Player.Instance.PlayerCombo());
        }
        
        if(messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;

        }

        comesFromCombo = false;
        Player.Instance.canAttack = true;
    }

    public override void MonsterEffect()
    {
        if(Player.Instance.isDefending == false)
        {
            if(Enemy.Instance.isCombo && isComboSkill)
            {
                Player.Instance.health += comboEffectValue * Enemy.Instance.boostAttack;
            }
            else
            {
                Player.Instance.health += effectValue * Enemy.Instance.boostAttack;
            }
        }
        Enemy.Instance.lastEnemyCompetence = this;

        if (!chargingAttack)
        {
            Enemy.Instance.StopCoroutine(Enemy.Instance.EnemyCombo());
            Enemy.Instance.StartCoroutine(Enemy.Instance.EnemyCombo());
        }

        if (messageType == typeOfMessage.Big)
        {
            messageType = typeOfMessage.Charging;
        }

        comesFromCombo = false;
        Enemy.Instance.canAttack = true;
    }
}

    

