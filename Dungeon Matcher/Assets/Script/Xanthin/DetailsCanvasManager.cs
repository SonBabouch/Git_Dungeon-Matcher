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
        //Update les Informations du 
        if(MenuManager.Instance.canvasManager.GetComponent<BagCanvasManager>().currentSelected != null)
        {
            //Description Stuff
            GameObject monsterToShow = MenuManager.Instance.canvasManager.GetComponent<BagCanvasManager>().currentSelected;
            monsterLevel.text = monsterToShow.GetComponent<Monster.MonsterToken>().monsterLevel.ToString();
            monsterName.text = monsterToShow.GetComponent<Monster.MonsterToken>().monsterName;
            monsterDescription.text = monsterToShow.GetComponent<Monster.MonsterToken>().description;
            fullMonsterImage.sprite = MenuManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().fullMonsterImage;


        }
    }
    
}
