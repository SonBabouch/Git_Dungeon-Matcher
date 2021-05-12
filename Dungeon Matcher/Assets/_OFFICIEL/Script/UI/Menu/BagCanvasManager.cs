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
    public GameObject detailsBackgroundBG;

    [SerializeField] private GameObject leftCross;
    [SerializeField] private GameObject rightCross;

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

        leftCross.SetActive(false);
        rightCross.SetActive(false);
    }
    private void Start()
    {
        currentButtonSelected = null;
        currentMonsterSelected = null;

        for (int i = 0; i < MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.allCommonMonster[i].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
            bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }

        for (int i = MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i < MenuManager.Instance.monsterEncyclopedie.allRareMonster.Count + MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.allRareMonster[i - MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
            bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }

        MenuManager.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
    }


    public void closeDetails()
    {
        Vector3 tweenScale = new Vector3(0, 0, 0);
        MenuManager.Instance.canvasManager.bagCanvas.detailsBackground.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);
        MenuManager.Instance.canvasManager.bagCanvas.detailsBackgroundBG.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);
        MenuManager.Instance.blockAction = false;
        MenuManager.Instance.bagManager.detailShow = false;
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

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 1)
        {
            leftCross.SetActive(true);
        }

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 2)
        {
            rightCross.SetActive(true);
        }

    }

    public void RemoveAll()
    {
        MenuManager.Instance.bagManager.monsterTeam[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
        equipeButton[0].sprite = defaultImage;
        MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
        MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[0]);
        equipeButton[1].sprite = defaultImage;
        UpdateEquipeButton();
        MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();

        
            leftCross.SetActive(false);
            equipeButton[0].sprite = defaultImage;
            rightCross.SetActive(false);
            equipeButton[1].sprite = defaultImage;
        
    }

    public void RemoveLeftEquipement()
    {
        if(MenuManager.Instance.bagManager.monsterTeam[0] != null)
        {
            MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[0]);

            UpdateEquipeButton();
            MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();

            
        }

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 0)
        {
            leftCross.SetActive(false);
            equipeButton[0].sprite = defaultImage;
        }

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 1)
        {
            rightCross.SetActive(false);
            equipeButton[1].sprite = defaultImage;
        }
    }

    public void RemoveRightEquipement()
    {
        if (MenuManager.Instance.bagManager.monsterTeam[1] != null)
        {
            MenuManager.Instance.bagManager.monsterTeam[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
            UpdateEquipeButton();
            MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();

            
        }

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 0)
        {
            leftCross.SetActive(false);
            equipeButton[0].sprite = defaultImage;
        }

        if (MenuManager.Instance.bagManager.monsterTeam.Count == 1)
        {
            rightCross.SetActive(false);
            equipeButton[1].sprite = defaultImage;
        }
    }
}
