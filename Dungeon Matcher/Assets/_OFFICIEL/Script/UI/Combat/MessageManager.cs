using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    public int conversationIndex;

    [Header("ArraysOfMessagesForPlayer")]
    [Header("ArraysOfPlayerAttackMessages")]
    [HideInInspector] public List<string> playerIndex1Attack = new List<string>();
    [HideInInspector] public List<string> playerIndex2Attack = new List<string>();
    [HideInInspector] public List<string> playerIndex3Attack = new List<string>();
    [HideInInspector] public List<string> playerIndex4Attack = new List<string>();
    [HideInInspector] public List<string> playerIndex5Attack = new List<string>();
    [Header("ArraysOfPlayerDefenseMessages")]
    [HideInInspector] public List<string> playerIndex1Defense = new List<string>();
    [HideInInspector] public List<string> playerIndex2Defense = new List<string>();
    [HideInInspector] public List<string> playerIndex3Defense = new List<string>();
    [HideInInspector] public List<string> playerIndex4Defense = new List<string>();
    [HideInInspector] public List<string> playerIndex5Defense = new List<string>();
    [Header("ArraysOfPlayerHealMessages")]
    [HideInInspector] public List<string> playerIndex1Heal = new List<string>();
    [HideInInspector] public List<string> playerIndex2Heal = new List<string>();
    [HideInInspector] public List<string> playerIndex3Heal = new List<string>();
    [HideInInspector] public List<string> playerIndex4Heal = new List<string>();
    [HideInInspector] public List<string> playerIndex5Heal = new List<string>();
    [Header("ArraysOfPlayerBreakMessages")]
    [HideInInspector] public List<string> playerIndex1Break = new List<string>();
    [HideInInspector] public List<string> playerIndex2Break = new List<string>();
    [HideInInspector] public List<string> playerIndex3Break = new List<string>();
    [HideInInspector] public List<string> playerIndex4Break = new List<string>();
    [HideInInspector] public List<string> playerIndex5Break = new List<string>();
    [Header("ArraysOfPlayerDrainMessages")]
    [HideInInspector] public List<string> playerIndex1Drain = new List<string>();
    [HideInInspector] public List<string> playerIndex2Drain = new List<string>();
    [HideInInspector] public List<string> playerIndex3Drain = new List<string>();
    [HideInInspector] public List<string> playerIndex4Drain = new List<string>();
    [HideInInspector] public List<string> playerIndex5Drain = new List<string>();
    [Header("ArraysOfPlayerParalysisMessages")]
    [HideInInspector] public List<string> playerIndex1Paralysis = new List<string>();
    [HideInInspector] public List<string> playerIndex2Paralysis = new List<string>();
    [HideInInspector] public List<string> playerIndex3Paralysis = new List<string>();
    [HideInInspector] public List<string> playerIndex4Paralysis = new List<string>();
    [HideInInspector] public List<string> playerIndex5Paralysis = new List<string>();
    [Header("ArraysOfPlayerCharmMessages")]
    [HideInInspector] public List<string> playerIndex1Charm = new List<string>();
    [HideInInspector] public List<string> playerIndex2Charm = new List<string>();
    [HideInInspector] public List<string> playerIndex3Charm = new List<string>();
    [HideInInspector] public List<string> playerIndex4Charm = new List<string>();
    [HideInInspector] public List<string> playerIndex5Charm = new List<string>();
    [Header("ArraysOfPlayerDivineTouchMessages")]
    [HideInInspector] public List<string> playerIndex1DivineTouch = new List<string>();
    [HideInInspector] public List<string> playerIndex2DivineTouch = new List<string>();
    [HideInInspector] public List<string> playerIndex3DivineTouch = new List<string>();
    [HideInInspector] public List<string> playerIndex4DivineTouch = new List<string>();
    [HideInInspector] public List<string> playerIndex5DivineTouch = new List<string>();
    [Header("ArraysOfPlayerWindstormMessages")]
    [HideInInspector] public List<string> playerIndex1Windstorm = new List<string>();
    [HideInInspector] public List<string> playerIndex2Windstorm = new List<string>();
    [HideInInspector] public List<string> playerIndex3Windstorm = new List<string>();
    [HideInInspector] public List<string> playerIndex4Windstorm = new List<string>();
    [HideInInspector] public List<string> playerIndex5Windstorm = new List<string>();
    [Header("ArraysOfPlayerChargedmMessages")]
    [HideInInspector] public List<string> playerIndex1Charged = new List<string>();
    [HideInInspector] public List<string> playerIndex2Charged = new List<string>();
    [HideInInspector] public List<string> playerIndex3Charged = new List<string>();
    [HideInInspector] public List<string> playerIndex4Charged = new List<string>();
    [HideInInspector] public List<string> playerIndex5Charged = new List<string>();

    [Header("ArraysOfMessagesForEnemy")]
    [Header("ArraysOfEnemyAttackMessages")]
    [HideInInspector] public List<string> enemyIndex1Attack = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Attack = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Attack = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Attack = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Attack = new List<string>();
    [Header("ArraysOfEnemyDefenseMessages")]
    [HideInInspector] public List<string> enemyIndex1Defense = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Defense = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Defense = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Defense = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Defense = new List<string>();
    [Header("ArraysOfEnemyHealMessages")]
    [HideInInspector] public List<string> enemyIndex1Heal = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Heal = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Heal = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Heal = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Heal = new List<string>();
    [Header("ArraysOfEnemyBreakMessages")]
    [HideInInspector] public List<string> enemyIndex1Break = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Break = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Break = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Break = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Break = new List<string>();
    [Header("ArraysOfEnemyDrainMessages")]
    [HideInInspector] public List<string> enemyIndex1Drain = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Drain = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Drain = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Drain = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Drain = new List<string>();
    [Header("ArraysOfEnemyParalysisMessages")]
    [HideInInspector] public List<string> enemyIndex1Paralysis = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Paralysis = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Paralysis = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Paralysis = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Paralysis = new List<string>();
    [Header("ArraysOfEnemyCharmMessages")]
    [HideInInspector] public List<string> enemyIndex1Charm = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Charm = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Charm = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Charm = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Charm = new List<string>();
    [Header("ArraysOfEnemyDivineTouchMessages")]
    [HideInInspector] public List<string> enemyIndex1DivineTouch = new List<string>();
    [HideInInspector] public List<string> enemyIndex2DivineTouch = new List<string>();
    [HideInInspector] public List<string> enemyIndex3DivineTouch = new List<string>();
    [HideInInspector] public List<string> enemyIndex4DivineTouch = new List<string>();
    [HideInInspector] public List<string> enemyIndex5DivineTouch = new List<string>();
    [Header("ArraysOfEnemyWindstormMessages")]
    [HideInInspector] public List<string> enemyIndex1Windstorm = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Windstorm = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Windstorm = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Windstorm = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Windstorm = new List<string>();
    [Header("ArraysOfEnemyChargedmMessages")]
    [HideInInspector] public List<string> enemyIndex1Charged = new List<string>();
    [HideInInspector] public List<string> enemyIndex2Charged = new List<string>();
    [HideInInspector] public List<string> enemyIndex3Charged = new List<string>();
    [HideInInspector] public List<string> enemyIndex4Charged = new List<string>();
    [HideInInspector] public List<string> enemyIndex5Charged = new List<string>();

    private List<string> chosenList = new List<string>();

    private void Awake()
    {
        conversationIndex = 0;
    }

    public void ChoseArray(Skill skill, GameObject Go)
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

        WriteMessages(Go);

    }
    public void WriteMessages(GameObject message)
    {
        int chosenText = Random.Range(0, chosenList.Count - 1);
        message.GetComponent<MessageBehaviour>().messageText.text = chosenList[chosenText];
        IncreaseIndex();
        Debug.Log(chosenList[chosenText]);
    }

    public void IncreaseIndex()
    {
        conversationIndex++;
        if(conversationIndex >= 5)
        {
            conversationIndex = 3;
        }
    }
}