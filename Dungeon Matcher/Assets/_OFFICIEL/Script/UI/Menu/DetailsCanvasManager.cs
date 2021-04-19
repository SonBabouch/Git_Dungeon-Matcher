using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Management;


/// <summary>
/// This script makes the visuel of Details Menu in Bag Menu.
/// </summary>

    
public class DetailsCanvasManager : MonoBehaviour
{
    //Description Générale
    [SerializeField] private TextMeshProUGUI monsterLevel;
    [SerializeField] private TextMeshProUGUI monsterName;
    [SerializeField] private TextMeshProUGUI monsterDescription;
    [SerializeField] private TextMeshProUGUI monsterAge;
    [SerializeField] private Image fullMonsterImage;

    //Skills Description
    [SerializeField] private Image[] skillTypeOf;
    [SerializeField] private Image[] skillCapacity;

    [SerializeField] private TextMeshProUGUI skill1Text;
    [SerializeField] private TextMeshProUGUI skill2Text;
    [SerializeField] private TextMeshProUGUI skill3Text;
    [SerializeField] private TextMeshProUGUI skill4Text;

    [SerializeField] private Sprite[] iconRessource;
    [SerializeField] private Sprite[] typeRessource;

    public void UpdateDetailsMenu()
    {
        //Update les Informations du Menu Details.
        if(MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected != null)
        {
            //Description Stuff
            GameObject monsterToShow = MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected;
            monsterLevel.text = "Level " + monsterToShow.GetComponent<MonsterToken>().monsterLevel.ToString();
            monsterName.text = monsterToShow.GetComponent<MonsterToken>().monsterName;
            monsterDescription.text = monsterToShow.GetComponent<MonsterToken>().description;
            fullMonsterImage.sprite = monsterToShow.GetComponent<MonsterToken>().fullMonsterImage;
            monsterAge.text = monsterToShow.GetComponent<MonsterToken>().health.ToString();

            //Description Skills
            skill1Text.text = monsterToShow.GetComponent<MonsterToken>().allySkills[0].skillDescription;
            skill2Text.text = monsterToShow.GetComponent<MonsterToken>().allySkills[1].skillDescription;
            skill3Text.text = monsterToShow.GetComponent<MonsterToken>().allySkills[2].skillDescription;
            skill4Text.text = monsterToShow.GetComponent<MonsterToken>().allySkills[3].skillDescription;

            for (int i = 0; i < skillTypeOf.Length; i++)
            {
                UpdateTypeOfImage(monsterToShow.GetComponent<MonsterToken>().allySkills[i],i);
                UpdateCapacityImage(monsterToShow.GetComponent<MonsterToken>().allySkills[i],i);
            }
            
        }
        
       
    }

    public void UpdateTypeOfImage(Skill monsterSkill, int currentIndex)
    {
        Sprite spriteToShow = null;

        switch (monsterSkill.typeOfCapacity)
        {
            case Skill.capacityType.Attack:
                spriteToShow = iconRessource[0];
                break;
            case Skill.capacityType.CoupDeVent:
                spriteToShow = iconRessource[1];
                break;
            case Skill.capacityType.Defense:
                spriteToShow = iconRessource[2];
                break;
            case Skill.capacityType.DivinTouch:
                spriteToShow = iconRessource[3];
                break;
            case Skill.capacityType.Drain:
                spriteToShow = iconRessource[4];
                break;
            case Skill.capacityType.Echo:
                spriteToShow = iconRessource[5];
                break;
            case Skill.capacityType.Heal:
                spriteToShow = iconRessource[6];
                break;
            case Skill.capacityType.Paralysie:
                spriteToShow = iconRessource[7];
                break;
            case Skill.capacityType.Plagiat:
                spriteToShow = iconRessource[8];
                break;
            case Skill.capacityType.Mark:
                spriteToShow = iconRessource[9];
                break;
            case Skill.capacityType.Curse:
                spriteToShow = iconRessource[10];
                break;
            case Skill.capacityType.Cramp:
                spriteToShow = iconRessource[11];
                break;
            case Skill.capacityType.Charm:
                spriteToShow = iconRessource[12];
                break;
            case Skill.capacityType.Silence:
                spriteToShow = iconRessource[13];
                break;
            case Skill.capacityType.Lock:
                spriteToShow = iconRessource[14];
                break;
            case Skill.capacityType.Break:
                spriteToShow = iconRessource[15];
                break;
            case Skill.capacityType.Ralentissement:
                spriteToShow = iconRessource[16];
                break;
            case Skill.capacityType.Acceleration:
                spriteToShow = iconRessource[17];
                break;
            case Skill.capacityType.Confuse:
                spriteToShow = iconRessource[18];
                break;
            default:
                break;
        }

        skillCapacity[currentIndex].sprite = spriteToShow;
    }

    public void UpdateCapacityImage(Skill monsterSkill, int currentIndex)
    {
        Sprite spriteToShow = null;

        switch (monsterSkill.messageType)
        {
            case Skill.typeOfMessage.Small:
                if (monsterSkill.isComboSkill)
                {
                    spriteToShow = typeRessource[0];
                }
                else
                {
                    spriteToShow = typeRessource[1]; 
                }
                break;
            case Skill.typeOfMessage.Big:
                spriteToShow = typeRessource[2];
                break;
            case Skill.typeOfMessage.Emoji:
                spriteToShow = typeRessource[3];
                break;

            default:
                break;
        }

        skillTypeOf[currentIndex].sprite = spriteToShow;
    }

}
