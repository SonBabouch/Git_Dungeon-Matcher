using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagCanvasManagerTuto : MonoBehaviour
{
    public GameObject currentButtonSelected;
    public GameObject currentMonsterSelected;

    public GameObject detailsBackground;
    public GameObject detailsBackgroundBG;

    [SerializeField] private GameObject bagButtonParent;
    public List<GameObject> bagButtonList = new List<GameObject>();

    public List<Image> equipeButton = new List<Image>();
    private Sprite defaultImage;

    [SerializeField] private GameObject leftCross;
    [SerializeField] private GameObject rightCross;

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
        leftCross.SetActive(false);
        rightCross.SetActive(false);

        currentButtonSelected = null;
        currentMonsterSelected = null;

        for (int i = 0; i < MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster[i].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
            bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }

        for (int i = MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster.Count; i < MenuManagerTuto.Instance.monsterEncyclopedie.allRareMonster.Count + MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManagerTuto.Instance.monsterEncyclopedie.allRareMonster[i - MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster.Count].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().profilPicture;
            bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }

        MenuManagerTuto.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        SortBag();
    }

    public void SortBag()
    {
        int currentPosition = 0;

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().isGet && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Common)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().isGet && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Common)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Common)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            if (bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible && bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                bagButtonList[i].transform.SetSiblingIndex(currentPosition);
                currentPosition++;
            }
        }

    }


    public void closeDetails()
    {
        Vector3 tweenScale = new Vector3(0, 0, 0);
        MenuManagerTuto.Instance.canvasManager.bagCanvas.detailsBackground.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);
        MenuManagerTuto.Instance.canvasManager.bagCanvas.detailsBackgroundBG.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);
        MenuManagerTuto.Instance.blockAction = false;
        MenuManagerTuto.Instance.bagManager.detailShow = false;
    }

    public void UpdateEquipeButton()
    {
        int repetition = 0;

        for (int i = 0; i < MenuManagerTuto.Instance.bagManager.monsterTeam.Count; i++)
        {
            equipeButton[i].sprite = MenuManagerTuto.Instance.bagManager.monsterTeam[i].GetComponent<MonsterToken>().profilPicture;
            repetition++;
        }

        if (repetition == 1)
        {
            equipeButton[1].sprite = defaultImage;
        }

    }

    public void RemoveAll()
    {
        MenuManagerTuto.Instance.bagManager.monsterTeam[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
        MenuManagerTuto.Instance.bagManager.monsterTeam.Remove(MenuManagerTuto.Instance.bagManager.monsterTeam[1]);
        equipeButton[0].sprite = defaultImage;
        MenuManagerTuto.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
        MenuManagerTuto.Instance.bagManager.monsterTeam.Remove(MenuManagerTuto.Instance.bagManager.monsterTeam[0]);
        equipeButton[1].sprite = defaultImage;
        UpdateEquipeButton();
        MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
        RemoveCross();
    }

    public void RemoveLeftEquipement()
    {
        if (MenuManagerTuto.Instance.bagManager.monsterTeam[0] != null)
        {
            MenuManagerTuto.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            MenuManagerTuto.Instance.bagManager.monsterTeam.Remove(MenuManagerTuto.Instance.bagManager.monsterTeam[0]);
            UpdateEquipeButton();
            MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
            RemoveCross();
        }

        if (MenuManagerTuto.Instance.bagManager.monsterTeam.Count == 0)
        {
            equipeButton[0].sprite = defaultImage;
        }

    }

    public void RemoveRightEquipement()
    {
        if (MenuManagerTuto.Instance.bagManager.monsterTeam[1] != null)
        {
            MenuManagerTuto.Instance.bagManager.monsterTeam[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            MenuManagerTuto.Instance.bagManager.monsterTeam.Remove(MenuManagerTuto.Instance.bagManager.monsterTeam[1]);
            UpdateEquipeButton();
            MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
            RemoveCross();
        }
    }

    public void RemoveCross()
    {
        switch (MenuManagerTuto.Instance.bagManager.monsterTeam.Count)
        {
            case 0:
                leftCross.SetActive(false);

                break;
            case 1:
                leftCross.SetActive(true);
                rightCross.SetActive(false);
                break;
            case 2:
                rightCross.SetActive(true);
                break;
            default:
                break;
        }
    }
}
    
