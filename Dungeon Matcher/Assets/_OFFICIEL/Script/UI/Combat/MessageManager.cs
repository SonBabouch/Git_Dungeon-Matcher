using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    public int conversationIndex;

    [Header("ArraysOfMessagesForPlayer")]
    [Header("ArraysOfPlayerAttackMessages")]
     public string[] playerIndex1Attack = new string[4];
     public string[] playerIndex2Attack = new string[4];
     public string[] playerIndex3Attack = new string[4];
     public string[] playerIndex4Attack = new string[4];
     public string[] playerIndex5Attack = new string[4];

    [Header("ArraysOfPlayerDefenseMessages")]
    [HideInInspector] public string[] playerIndex1Defense = new string[4];
    [HideInInspector] public string[] playerIndex2Defense = new string[4];
    [HideInInspector] public string[] playerIndex3Defense = new string[4];
    [HideInInspector] public string[] playerIndex4Defense = new string[4];
    [HideInInspector] public string[] playerIndex5Defense = new string[4];

    [Header("ArraysOfPlayerHealMessages")]
    [HideInInspector] public string[] playerIndex1Heal = new string[4];
    [HideInInspector] public string[] playerIndex2Heal = new string[4];
    [HideInInspector] public string[] playerIndex3Heal = new string[4];
    [HideInInspector] public string[] playerIndex4Heal = new string[4];
    [HideInInspector] public string[] playerIndex5Heal = new string[4];

    [Header("ArraysOfPlayerBreakMessages")]
    [HideInInspector] public string[] playerIndex1Break = new string[4];
    [HideInInspector] public string[] playerIndex2Break = new string[4];
    [HideInInspector] public string[] playerIndex3Break = new string[4];
    [HideInInspector] public string[] playerIndex4Break = new string[4];
    [HideInInspector] public string[] playerIndex5Break = new string[4];
    [Header("ArraysOfPlayerDrainMessages")]
    [HideInInspector] public string[] playerIndex1Drain = new string[4];
    [HideInInspector] public string[] playerIndex2Drain = new string[4];
    [HideInInspector] public string[] playerIndex3Drain = new string[4];
    [HideInInspector] public string[] playerIndex4Drain = new string[4];
    [HideInInspector] public string[] playerIndex5Drain = new string[4];
    [Header("ArraysOfPlayerParalysisMessages")]
    [HideInInspector] public string[] playerIndex1Paralysis = new string[4];
    [HideInInspector] public string[] playerIndex2Paralysis = new string[4];
    [HideInInspector] public string[] playerIndex3Paralysis = new string[4];
    [HideInInspector] public string[] playerIndex4Paralysis = new string[4];
    [HideInInspector] public string[] playerIndex5Paralysis = new string[4];
    [Header("ArraysOfPlayerCharmMessages")]
    [HideInInspector] public string[] playerIndex1Charm = new string[4];
    [HideInInspector] public string[] playerIndex2Charm = new string[4];
    [HideInInspector] public string[] playerIndex3Charm = new string[4];
    [HideInInspector] public string[] playerIndex4Charm = new string[4];
    [HideInInspector] public string[] playerIndex5Charm = new string[4];

    [Header("ArraysOfPlayerDivineTouchMessages")]
    [HideInInspector] public string[] playerIndex1DivineTouch = new string[4];
    [HideInInspector] public string[] playerIndex2DivineTouch = new string[4];
    [HideInInspector] public string[] playerIndex3DivineTouch = new string[4];
    [HideInInspector] public string[] playerIndex4DivineTouch = new string[4];
    [HideInInspector] public string[] playerIndex5DivineTouch = new string[4];

    [Header("ArraysOfPlayerWindstormMessages")]
    [HideInInspector] public string[] playerIndex1Windstorm = new string[4];
    [HideInInspector] public string[] playerIndex2Windstorm = new string[4];
    [HideInInspector] public string[] playerIndex3Windstorm = new string[4];
    [HideInInspector] public string[] playerIndex4Windstorm = new string[4];
    [HideInInspector] public string[] playerIndex5Windstorm = new string[4];

    [Header("ArraysOfPlayerChargedmMessages")]
    /*[HideInInspector]*/ public string[] playerIndex1Charged = new string[4];
    [HideInInspector] public string[] playerIndex2Charged = new string[4];
    [HideInInspector] public string[] playerIndex3Charged = new string[4];
    [HideInInspector] public string[] playerIndex4Charged = new string[4];
    [HideInInspector] public string[] playerIndex5Charged = new string[4];

    [Header("ArraysOfMessagesForEnemy")]
    [Header("ArraysOfEnemyAttackMessages")]
    [HideInInspector] public string[] enemyIndex1Attack = new string[4];
    [HideInInspector] public string[] enemyIndex2Attack = new string[4];
    [HideInInspector] public string[] enemyIndex3Attack = new string[4];
    [HideInInspector] public string[] enemyIndex4Attack = new string[4];
    [HideInInspector] public string[] enemyIndex5Attack = new string[4];

    [Header("ArraysOfEnemyDefenseMessages")]
    [HideInInspector] public string[] enemyIndex1Defense = new string[4];
    [HideInInspector] public string[] enemyIndex2Defense = new string[4];
    [HideInInspector] public string[] enemyIndex3Defense = new string[4];
    [HideInInspector] public string[] enemyIndex4Defense = new string[4];
    [HideInInspector] public string[] enemyIndex5Defense = new string[4];
    [Header("ArraysOfEnemyHealMessages")]
    [HideInInspector] public string[] enemyIndex1Heal = new string[4];
    [HideInInspector] public string[] enemyIndex2Heal = new string[4];
    [HideInInspector] public string[] enemyIndex3Heal = new string[4];
    [HideInInspector] public string[] enemyIndex4Heal = new string[4];
    [HideInInspector] public string[] enemyIndex5Heal = new string[4];
    [Header("ArraysOfEnemyBreakMessages")]
    [HideInInspector] public string[] enemyIndex1Break = new string[4];
    [HideInInspector] public string[] enemyIndex2Break = new string[4];
    [HideInInspector] public string[] enemyIndex3Break = new string[4];
    [HideInInspector] public string[] enemyIndex4Break = new string[4];
    [HideInInspector] public string[] enemyIndex5Break = new string[4];

    [Header("ArraysOfEnemyDrainMessages")]
    [HideInInspector] public string[] enemyIndex1Drain = new string[4];
    [HideInInspector] public string[] enemyIndex2Drain = new string[4];
    [HideInInspector] public string[] enemyIndex3Drain = new string[4];
    [HideInInspector] public string[] enemyIndex4Drain = new string[4];
    [HideInInspector] public string[] enemyIndex5Drain = new string[4];
    [Header("ArraysOfEnemyParalysisMessages")]
    [HideInInspector] public string[] enemyIndex1Paralysis = new string[4];
    [HideInInspector] public string[] enemyIndex2Paralysis = new string[4];
    [HideInInspector] public string[] enemyIndex3Paralysis = new string[4];
    [HideInInspector] public string[] enemyIndex4Paralysis = new string[4];
    [HideInInspector] public string[] enemyIndex5Paralysis = new string[4];

    [Header("ArraysOfEnemyCharmMessages")]
    [HideInInspector] public string[] enemyIndex1Charm = new string[4];
    [HideInInspector] public string[] enemyIndex2Charm = new string[4];
    [HideInInspector] public string[] enemyIndex3Charm = new string[4];
    [HideInInspector] public string[] enemyIndex4Charm = new string[4];
    [HideInInspector] public string[] enemyIndex5Charm = new string[4];

    [Header("ArraysOfEnemyDivineTouchMessages")]
    [HideInInspector] public string[] enemyIndex1DivineTouch = new string[4];
    [HideInInspector] public string[] enemyIndex2DivineTouch = new string[4];
    [HideInInspector] public string[] enemyIndex3DivineTouch = new string[4];
    [HideInInspector] public string[] enemyIndex4DivineTouch = new string[4];
    [HideInInspector] public string[] enemyIndex5DivineTouch = new string[4];

    [Header("ArraysOfEnemyWindstormMessages")]
    [HideInInspector] public string[] enemyIndex1Windstorm = new string[4];
    [HideInInspector] public string[] enemyIndex2Windstorm = new string[4];
    [HideInInspector] public string[] enemyIndex3Windstorm = new string[4];
    [HideInInspector] public string[] enemyIndex4Windstorm = new string[4];
    [HideInInspector] public string[] enemyIndex5Windstorm = new string[4];

    public string[] cursedMessagePlayer = new string[4];
    [HideInInspector] public string[] cursedMessageMonster = new string[4];

    [Header("ArraysOfEnemyChargedmMessages")]
    [HideInInspector] public string[] enemyIndex1Charged = new string[4];
    [HideInInspector] public string[] enemyIndex2Charged = new string[4];
    [HideInInspector] public string[] enemyIndex3Charged = new string[4];
    [HideInInspector] public string[] enemyIndex4Charged = new string[4];
    [HideInInspector] public string[] enemyIndex5Charged = new string[4];


    private string[] chosenList;

    private void Awake()
    {
        instance = this;
        conversationIndex = 0;
    }

    public void ChoseArray(Skill skill, GameObject Go)
    {
        if (skill.comesFromCurse)
        {
            Debug.Log("cursedMessage");
            skill.comesFromCurse = false;
            chosenList = cursedMessagePlayer;
        }
        else
        {
            switch (skill.side)
            {
                case Skill.monsterSide.Enemy:
                    if (skill.typeOfCapacity == Skill.capacityType.Echo)
                    {
                        skill.typeOfCapacity = Enemy.Instance.lastEnemyCompetence.typeOfCapacity;
                    }
                    if (skill.typeOfCapacity == Skill.capacityType.Plagiat)
                    {
                        skill.typeOfCapacity = Player.Instance.lastPlayerCompetence.typeOfCapacity;
                    }

                    if (skill.chargingAttack)
                    {
                        switch (conversationIndex)
                        {
                            case 0:
                                chosenList = enemyIndex1Charged;
                                break;
                            case 1:
                                chosenList = enemyIndex2Charged;
                                break;
                            case 2:
                                chosenList = enemyIndex3Charged;
                                break;
                            case 3:
                                chosenList = enemyIndex4Charged;
                                break;
                            case 4:
                                chosenList = enemyIndex5Charged;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (skill.typeOfCapacity)
                        {
                            //[MÉMO À MOI-MÊME] L'ordre est : Attack - Defense - Heal - Break - Drain - Paralysie - Charm - DivinTouch - CoupDeVent.
                            case Skill.capacityType.Attack:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Attack;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Attack;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Attack;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Attack;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Attack;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Defense:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Defense;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Defense;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Defense;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Defense;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Defense;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Heal:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Heal;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Heal;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Heal;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Heal;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Heal;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Break:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Break;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Break;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Break;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Break;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Break;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Drain:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Drain;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Drain;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Drain;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Drain;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Drain;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Paralysie:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Paralysis;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Paralysis;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Paralysis;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Paralysis;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Paralysis;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Charm:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Charm;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Charm;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Charm;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Charm;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Charm;
                                        break;
                                }
                                break;
                            case Skill.capacityType.DivinTouch:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1DivineTouch;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2DivineTouch;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3DivineTouch;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4DivineTouch;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5DivineTouch;
                                        break;
                                }
                                break;
                            case Skill.capacityType.CoupDeVent:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = enemyIndex1Windstorm;
                                        break;
                                    case 1:
                                        chosenList = enemyIndex2Windstorm;
                                        break;
                                    case 2:
                                        chosenList = enemyIndex3Windstorm;
                                        break;
                                    case 3:
                                        chosenList = enemyIndex4Windstorm;
                                        break;
                                    case 4:
                                        chosenList = enemyIndex5Windstorm;
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    break;


                case Skill.monsterSide.Ally:

                    if (skill.typeOfCapacity == Skill.capacityType.Echo)
                    {
                        skill.typeOfCapacity = Player.Instance.lastPlayerCompetence.typeOfCapacity;
                    }
                    if (skill.typeOfCapacity == Skill.capacityType.Plagiat)
                    {
                        skill.typeOfCapacity = Enemy.Instance.lastEnemyCompetence.typeOfCapacity;
                    }

                    //on regarde d'abord si l'attaque est chargée ou non
                    if (skill.chargingAttack)
                    {
                        switch (conversationIndex)
                        {
                            case 0:
                                chosenList = playerIndex1Charged;
                                break;
                            case 1:
                                chosenList = playerIndex2Charged;
                                break;
                            case 2:
                                chosenList = playerIndex3Charged;
                                break;
                            case 3:
                                chosenList = playerIndex4Charged;
                                break;
                            case 4:
                                chosenList = playerIndex5Charged;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (skill.typeOfCapacity)
                        {
                            //[MÉMO À MOI-MÊME] L'ordre est : Attack - Defense - Heal - Break - Drain - Paralysie - Charm - DivinTouch - CoupDeVent.
                            case Skill.capacityType.Attack:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Attack;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Attack;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Attack;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Attack;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Attack;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Defense:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Defense;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Defense;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Defense;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Defense;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Defense;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Heal:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Heal;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Heal;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Heal;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Heal;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Heal;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Break:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Break;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Break;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Break;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Break;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Break;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Drain:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Drain;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Drain;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Drain;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Drain;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Drain;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Paralysie:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Paralysis;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Paralysis;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Paralysis;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Paralysis;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Paralysis;
                                        break;
                                }
                                break;
                            case Skill.capacityType.Charm:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Charm;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Charm;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Charm;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Charm;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Charm;
                                        break;
                                }
                                break;
                            case Skill.capacityType.DivinTouch:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1DivineTouch;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2DivineTouch;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3DivineTouch;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4DivineTouch;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5DivineTouch;
                                        break;
                                }
                                break;
                            case Skill.capacityType.CoupDeVent:
                                switch (conversationIndex)
                                {
                                    case 0:
                                        chosenList = playerIndex1Windstorm;
                                        break;
                                    case 1:
                                        chosenList = playerIndex2Windstorm;
                                        break;
                                    case 2:
                                        chosenList = playerIndex3Windstorm;
                                        break;
                                    case 3:
                                        chosenList = playerIndex4Windstorm;
                                        break;
                                    case 4:
                                        chosenList = playerIndex5Windstorm;
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        WriteMessages(Go);
    }
    public void WriteMessages(GameObject message)
    {
        int chosenText = Random.Range(0, chosenList.Length - 1);
        message.GetComponent<MessageBehaviour>().messageText.text = chosenList[chosenText];
        IncreaseIndex();
        //Debug.Log(chosenList[chosenText]);
    }

    public void IncreaseIndex()
    {
        conversationIndex++;
        if(conversationIndex >= 5)
        {
            conversationIndex = 2;
        }
    }
}