using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

    [Header("ArrayOfMessages")]
    [SerializeField] private string[] attackMessages;
    [SerializeField] private string[] attackMessagesCombo;
    [SerializeField] private string[] healMessages;
    [SerializeField] private string[] defenseMessages;
    [SerializeField] private string[] divinTouchMessages;
    [SerializeField] private string[] drainMessages;
    [SerializeField] private string[] echoMessages;
    [SerializeField] private string[] paralyseMessages;
    [SerializeField] private string[] plagiatMessages;
    [SerializeField] private string[] coupDeVentMessages;

    private string[] chosenList;

    public void ChoseArray(Skill skill, GameObject Go)
    {
        switch (skill.typeOfCapacity)
        {
            //Attention car attack peut être combo+Chargée.
            case Skill.capacityType.Attack:
                if (skill.isComboSkill)
                {
                    //=> Tableau Combo.
                }
                else
                {
                    chosenList = attackMessages;
                }
                break;
            case Skill.capacityType.CoupDeVent:
                chosenList = coupDeVentMessages;
                break;
            case Skill.capacityType.Defense:
                chosenList = defenseMessages;
                break;
            case Skill.capacityType.DivinTouch:
                chosenList = divinTouchMessages;
                break;
            case Skill.capacityType.Drain:
                chosenList = drainMessages;
                break;
            case Skill.capacityType.Echo:
                chosenList = echoMessages;
                break;
            case Skill.capacityType.Heal:
                if (skill.isComboSkill)
                {
                    //=> Tableau Combo Heal.
                }
                else
                {
                    chosenList = healMessages;
                }
                break;
            case Skill.capacityType.Paralysie:
                chosenList = paralyseMessages;
                break;
            case Skill.capacityType.Plagiat:
                chosenList = plagiatMessages;
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
