using UnityEngine;
using System.Collections.Generic;
using Management;
using UnityEngine.UI;

/// <summary>
/// Ce script permet de gérer le visuel de l'inventaire.
/// </summary>
public class BagCanvasManager : MonoBehaviour
{
    public GameObject currentButtonSelected;
    public GameObject currentMonsterSelected;

    public GameObject detailsBackground;

    [SerializeField] private GameObject bagButtonParent;
    public List<GameObject> bagButtonList = new List<GameObject>();

    public List<Image> equipeButton = new List<Image>();
    private Sprite defaultImage;
    private void Awake()
    {
        foreach (Transform child in bagButtonParent.transform)
        {
            bagButtonList.Add(child.gameObject);
        }
        defaultImage = equipeButton[0].GetComponent<Image>().sprite;

       
    }
    private void Start()
    {
        currentButtonSelected = null;
        currentMonsterSelected = null;

        for (int i = 0; i < MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.allCommonMonster[i].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
        }

        for (int i = MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i < MenuManager.Instance.monsterEncyclopedie.allRareMonster.Count + MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {

            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.allRareMonster[i - MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
        }

        MenuManager.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
    }


    public void closeDetails()
    {
        detailsBackground.SetActive(false);
        MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = false;
    }

    public void UpdateEquipeButton()
    {
        int repetition = 0;

        for (int i = 0; i < MenuManager.Instance.bagManager.monsterTeam.Count; i++)
        {
            equipeButton[i].sprite = MenuManager.Instance.bagManager.monsterTeam[i].GetComponent<MonsterToken>().profilPicture;
            repetition++;
        }

        if(repetition == 1)
        {
            equipeButton[1].sprite = defaultImage;
        }
        
    }

    public void RemoveLeftEquipement()
    {
        MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[0]);
        UpdateEquipeButton();
    }

    public void RemoveRightEquipement()
    {
        MenuManager.Instance.bagManager.monsterTeam[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
        UpdateEquipeButton();
    }
}
