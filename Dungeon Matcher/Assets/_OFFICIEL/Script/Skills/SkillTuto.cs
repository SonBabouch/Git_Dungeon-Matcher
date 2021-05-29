using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bastien Prigent
/// </summary>
public abstract class SkillTuto : ScriptableObject
{
    public string skillName;

    [Header("Common")]
    public string skillDescription;
    public float effectValue;

    public GameObject messageOwner;

    public enum monsterSide { Enemy, Ally }
    public monsterSide side;

    public enum typeOfMessage { Small, Big, Emoji, Charging }
    public typeOfMessage messageType;

    public enum capacityType { Attack, Heal, Defense, Paralysie, DivinTouch, CoupDeVent, Drain, Echo, Plagiat, Mark, Curse, Cramp, Charm, Silence, Lock, Break, Slowdown, Acceleration, Confuse };
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
    public abstract void PlaySound();

    public void realUse()
    {
        if (ConversationManagerTuto.Instance.canAttack)
        {
            switch (side)
            {
                case monsterSide.Enemy:
                    if (EnemyTuto.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (!EnemyTuto.Instance.isCharging && EnemyTuto.Instance.canAttack && EnemyTuto.Instance.energy >= energyCost)
                    {
                        EnemyTuto.Instance.canAttack = false;

                        if (EnemyTuto.Instance.isCombo && isComboSkill)
                        {
                            comesFromCombo = true;
                        }

                        //Test Curse
                        if (EnemyTuto.Instance.isCurse)
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
                            if (EnemyTuto.Instance.energy >= energyCost)
                            {
                                EnemyTuto.Instance.energy -= energyCost;
                                EnemyTuto.Instance.trueEnergy -= trueEnergyCost;

                                //ici ca sera Enemy plutot que player
                                EnemyTuto.Instance.StartCoroutine(Enemy.Instance.EnemyChargeAttack(this));
                            }
                        }
                        else
                        {
                            InUse();
                        }

                    }
                    break;

                case monsterSide.Ally:

                    if (PlayerTuto.Instance.isCramp)
                    {
                        energyCost = crampEnergyCost;
                    }
                    else
                    {
                        energyCost = initialEnergyCost;
                    }

                    if (PlayerTuto.Instance.canAttack && PlayerTuto.Instance.isCharging == false && PlayerTuto.Instance.energy >= energyCost)
                    {
                        if (PlayerTuto.Instance.isCombo && isComboSkill)
                        {
                            comesFromCombo = true;
                        }

                        if (PlayerTuto.Instance.isCramp)
                        {
                            energyCost = crampEnergyCost;
                        }
                        else
                        {
                            energyCost = initialEnergyCost;
                        }

                        if (PlayerTuto.Instance.isCurse)
                        {
                            int test = Random.Range(0, 100);

                            if (test < 70)
                            {
                                //Debug.Log("Cursed");
                                comesFromCurse = true;
                                //CombatManager.Instance.ButtonsUpdate();
                                InUse();
                            }
                            else
                            {
                                comesFromCurse = false;
                                //Debug.Log("PasCursed");
                            }
                            
                        }
                        if (chargingAttack)
                        {
                            if (PlayerTuto.Instance.energy >= energyCost)
                            {
                                PlayerTuto.Instance.energy -= energyCost;
                                PlayerTuto.Instance.trueEnergy -= trueEnergyCost;

                                //ici ca sera Enemy plutot que player
                                PlayerTuto.Instance.StartCoroutine(Player.Instance.PlayerChargeAttack(this));
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

                if (PlayerTuto.Instance.energy >= energyCost)
                {
                    PlayerTuto.Instance.energy -= energyCost;
                    PlayerTuto.Instance.trueEnergy -= trueEnergyCost;

                    if (PlayerTuto.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }


                    CombatManagerTuto.Instance.ButtonsUpdate();
                    ConversationManagerTuto.Instance.SendMessagesPlayer(this, index);
                }
                break;

            case monsterSide.Enemy:
                if (EnemyTuto.Instance.energy >= energyCost)
                {
                    EnemyTuto.Instance.energy -= energyCost;
                    EnemyTuto.Instance.trueEnergy -= trueEnergyCost;

                    if (EnemyTuto.Instance.isCramp)
                    {
                        energyCost = initialEnergyCost;
                    }


                    EnemyTuto.Instance.EnemySwapSkill(EnemyTuto.Instance.enemyIndex);
                    ConversationManagerTuto.Instance.SendMessagesEnemy(this, index);

                }
                break;
        }
        EnemyTuto.Instance.enemyIndex = 0;
    }

    public void UpdateEchoValue()
    {
        if (side == monsterSide.Ally)
        {
            lastCompetenceReference = PlayerTuto.Instance.lastPlayerCompetence;
        }
        else
        {
            lastCompetenceReference = EnemyTuto.Instance.lastEnemyCompetence;
        }


    }

    public void UpdatePlagiatValue()
    {
        if (side == monsterSide.Ally)
        {
            lastCompetenceReference = EnemyTuto.Instance.lastEnemyCompetence;

        }
        else
        {
            lastCompetenceReference = PlayerTuto.Instance.lastPlayerCompetence;
        }
    }
}

