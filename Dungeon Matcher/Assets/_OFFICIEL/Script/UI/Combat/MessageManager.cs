using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

    [Header("ArrayOfMessages")]
    [SerializeField] private string[] attackMessagesPlayer;
    [SerializeField] private string[] attackMessagesComboPlayer;
    [SerializeField] private string[] healMessagesPlayer;
    [SerializeField] private string[] defenseMessagesPlayer;
    [SerializeField] private string[] divinTouchMessagesPlayer;
    [SerializeField] private string[] drainMessagesPlayer;
    [SerializeField] private string[] echoMessagesPlayer;
    [SerializeField] private string[] paralyseMessagesPlayer;
    [SerializeField] private string[] plagiatMessagesPlayer;
    [SerializeField] private string[] coupDeVentMessagesPlayer;

    private string[] chosenList;

    public void ChoseArray(Skill skill, GameObject Go)
    {
        switch (skill.side)
        {
            case Skill.monsterSide.Enemy:
                if (skill.typeOfCapacity == Skill.capacityType.Echo)
                {
                    skill.typeOfCapacity = Enemy.Instance.lastEnemyCompetence.typeOfCapacity;
                }

                //Faire pareil pour l'enemy
                break;

            case Skill.monsterSide.Ally:

                if(skill.typeOfCapacity == Skill.capacityType.Echo)
                {
                    skill.typeOfCapacity = Player.Instance.lastPlayerCompetence.typeOfCapacity;
                }

                switch (skill.typeOfCapacity)
                {
                    //Attention car attack peut être combo+Chargée.
                    case Skill.capacityType.Attack:
                        if (skill.isComboSkill && skill.comesFromCombo && Player.Instance.isCombo && Player.Instance.lastPlayerCompetence != null)
                        {
                            chosenList = attackMessagesComboPlayer;
                        }
                        else
                        {
                            chosenList = attackMessagesPlayer;
                        }
                        break;
                    case Skill.capacityType.CoupDeVent:
                        chosenList = coupDeVentMessagesPlayer;
                        break;
                    case Skill.capacityType.Defense:
                        chosenList = defenseMessagesPlayer;
                        break;
                    case Skill.capacityType.DivinTouch:
                        chosenList = divinTouchMessagesPlayer;
                        break;
                    case Skill.capacityType.Drain:
                        chosenList = drainMessagesPlayer;
                        break;
                    case Skill.capacityType.Echo:
                        chosenList = echoMessagesPlayer;
                        break;
                    case Skill.capacityType.Heal:
                        if (skill.isComboSkill && skill.comesFromCombo && Player.Instance.isCombo)
                        {
                            //=> Tableau Combo Heal.
                        }
                        else
                        {
                            chosenList = healMessagesPlayer;
                        }
                        break;
                    case Skill.capacityType.Paralysie:
                        chosenList = paralyseMessagesPlayer;
                        break;
                    case Skill.capacityType.Plagiat:
                        chosenList = plagiatMessagesPlayer;
                        break;

                    default:
                        break;
                }
                break;
            default:
                break;
        }
        

        WriteMessages(Go);
    }


    public void WriteMessages(GameObject message)
    {
        int chosenText = Random.Range(0, chosenList.Length);
        message.GetComponent<MessageBehaviour>().messageText.text = chosenList[chosenText];
    }
}
