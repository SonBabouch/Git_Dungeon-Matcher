using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bastien Prigent
/// </summary>
public abstract class Skill : ScriptableObject
{
    [Header("Common")]
    public string skillDescription;
    public float effectValue;

    public GameObject messageOwner;

    public enum monsterSide { Enemy, Ally }
    public monsterSide side;

    public enum typeOfMessage { Small, Big, Emoji, Charging }
    public typeOfMessage messageType;

    public enum capacityType { Attack, Heal, Defense, Paralysie, DivinTouch, CoupDeVent, Drain, Echo, Plagiat, Mark, Curse, Cramp, Charm, Silence, Lock, Break, Ralentissement, Acceleration, Confuse };
    public capacityType typeOfCapacity;

    [Header("Energy Cost")]
    public int trueEnergyCost;
    public int energyCost;
    public int initialEnergyCost;
    public int crampEnergyCost;

    [Header("TypeOfCapacity")]
    public bool isComboSkill = false;
    public bool comesFromCombo = false;
    public bool comesFromCurse = false;
    public float comboEffectValue;
    public bool chargingAttack;

    public bool isEcho;
    public bool isPlagiat;

    public Skill lastCompetenceReference;

    public int skillIndex;
    public abstract void SetEnemyBoolType();
    public abstract void Initialize(GameObject obj);
    public abstract void Use();
    public abstract void PlayerEffect();
    public abstract void MonsterEffect();
    public abstract void InUse();

    public void realUse()
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

                    if (!Enemy.Instance.isCharging && Enemy.Instance.canAttack && Enemy.Instance.energy >= energyCost)
                    {
                        Enemy.Instance.canAttack = false;

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
                                comesFromCurse = true;
                                //switch la carte de la main de l'enemy;
                                
                            }
                            comesFromCurse = false;
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
                            //Debug.Log(test);
                            if (test < 70)
                            {
                                Debug.Log("Cursed");
                                comesFromCurse = true;
                                CombatManager.Instance.ButtonsUpdate();
                                InUse();
                            }
                            else
                            {
                                comesFromCurse = false;
                                Debug.Log("PasCursed");
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
    public void realInUse(int index)
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
                    ConversationManager.Instance.SendMessagesPlayer(this, index);

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
                    ConversationManager.Instance.SendMessagesEnemy(this, index);

                }
                break;
        }
        CombatManager.Instance.index = 0;
        Enemy.Instance.enemyIndex = 0;
    }

    public void UpdateEchoValue()
    {
        if (side == monsterSide.Ally)
        {
            lastCompetenceReference = Player.Instance.lastPlayerCompetence;
        }
        else
        {
            lastCompetenceReference = Enemy.Instance.lastEnemyCompetence;
        }


    }

    public void UpdatePlagiatValue()
    {
        if (side == monsterSide.Ally)
        {
            lastCompetenceReference = Enemy.Instance.lastEnemyCompetence;

        }
        else
        {
            lastCompetenceReference = Player.Instance.lastPlayerCompetence;
        }
    }
}

