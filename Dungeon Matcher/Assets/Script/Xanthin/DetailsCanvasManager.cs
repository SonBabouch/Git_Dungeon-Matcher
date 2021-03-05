﻿using System.Collections;
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
    [SerializeField] private Image skill1Image;
    [SerializeField] private Image skill2Image;
    [SerializeField] private Image skill3Image;
    [SerializeField] private Image skill4Image;
    [SerializeField] private TextMeshProUGUI skill1Text;
    [SerializeField] private TextMeshProUGUI skill2Text;
    [SerializeField] private TextMeshProUGUI skill3Text;
    [SerializeField] private TextMeshProUGUI skill4Text;

    public void UpdateDetailsMenu()
    {
        //Update les Informations du Menu Details.
        if(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected != null)
        {
            //Description Stuff
            GameObject monsterToShow = MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected;
            monsterLevel.text = monsterToShow.GetComponent<Monster.MonsterToken>().monsterLevel.ToString();
            monsterName.text = monsterToShow.GetComponent<Monster.MonsterToken>().monsterName;
            monsterDescription.text = monsterToShow.GetComponent<Monster.MonsterToken>().description;
            fullMonsterImage.sprite = monsterToShow.GetComponent<Monster.MonsterToken>().profilPicture;


            /*Description Skills
            skill1Text.text = monsterToShow.GetComponent<Monster.MonsterToken>().skills[0].skillName;
            skill2Text.text = monsterToShow.GetComponent<Monster.MonsterToken>().skills[1].skillName;
            skill3Text.text = monsterToShow.GetComponent<Monster.MonsterToken>().skills[2].skillName;
            skill4Text.text = monsterToShow.GetComponent<Monster.MonsterToken>().skills[3].skillName;

            skill1Image.sprite = monsterToShow.GetComponent<Monster.MonsterToken>().skills[0].skillImage;
            skill2Image.sprite = monsterToShow.GetComponent<Monster.MonsterToken>().skills[1].skillImage;
            skill3Image.sprite = monsterToShow.GetComponent<Monster.MonsterToken>().skills[2].skillImage;
            skill4Image.sprite = monsterToShow.GetComponent<Monster.MonsterToken>().skills[3].skillImage;
            */
        }
        
       
    }

    public void RemoveLeftEquipement()
    {
        MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<Monster.MonsterToken>().statement = Monster.MonsterToken.statementEnum.Disponible;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[0]);

       
    }

    public void RemoveRightEquipement()
    {
        MenuManager.Instance.bagManager.monsterTeam[1].GetComponent<Monster.MonsterToken>().statement = Monster.MonsterToken.statementEnum.Disponible;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
    }

}